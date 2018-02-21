// -*- Mode: c++; tab-width: 2; indent-tabs-mode: nil; c-basic-offset: 2 -*-
//
// Copyright (C) 2016 Opera Software AS. All rights reserved.
//
// This file is an original work developed by Opera Software AS

'use strict';

class FilterContent {
    constructor() {
        this.filteredElements_ = new WeakSet();
        this.observer_ = null;
        this.root_ = null;
        this.selectors_ = new Map();
        this.styles_ = document.createElement('style');
        this.styles_.type = 'text/css';
        this.urls_ = new Map();

        setTimeout(() => {
            if (!opr.contentFilterPrivate.isWhitelisted()) {
                this.whenDomReady_().then(() => this.initialize_());
                this.applySelectors_();
            }
        }, 0);
    }

    applySelectors_() {
        return new Promise(resolve => {
            opr.contentFilterPrivate.getBlockedSelectors(selectors => {
                if (!Array.isArray(selectors)) {
                    resolve();
                    return;
                }
                let styles = [];
                while (selectors.length) {
                    styles.push(`:root ${selectors.splice(0, 1000).join(', :root ')}`);
                }

                this.styles_.textContent =
                    styles.map(selector => `${selector} { display: none !important; }`)
                        .join('\n');
                resolve();
            });
        });
    }

    fetchSelectors_(element) {
        let selectors = [];
        if (element.id) {
            selectors.push(`#${element.id}`);
        }

        if (element.classList) {
            for (let cl of element.classList) {
                selectors.push(`.${cl}`);
            }
        }

        return selectors;
    }

    filter_() {
        if (!this.root_) {
            return;
        }

        this.filterChildrenIfNeeded_(this.root_);
        this.filterAssetsIfNeeded_(this.root_);
    }

    filterAssetsIfNeeded_(root) {
        let assets = root.querySelectorAll('[src]');
        for (let asset of assets) {
            if (asset.src && this.isUrlFiltered_(asset.src)) {
                this.hideElement_(asset);
            }
        }
    }

    filterChildrenIfNeeded_(parent) {
        if (parent.querySelectorAll) {
            let children = parent.querySelectorAll('[id], [class]');
            for (let i = 0; i < children.length; i++) {
                this.filterElementIfNeeded_(children[i]);
            }
        }
    }

    filterElementIfNeeded_(element) {
        if (!element) {
            return false;
        }

        if (this.filteredElements_.has(element)) {
            return true;
        }

        if (this.hasForbidenSelectors_(element)) {
            this.hideElement_(element);
            opr.contentFilterPrivate.recordBlockActions(1);
            return true;
        }

        return false;
    }

    hasForbidenSelectors_(element, callback) {
        const selectors = this.fetchSelectors_(element);
        const newSelectors = [];
        let selector;
        let value;

        for (let i = 0; i < selectors.length; i++) {
            selector = selectors[i];
            value = this.selectors_.get(selector);
            if (value) {
                return true;
            }

            if (value === undefined) {
                newSelectors.push(selector);
            }
        }

        for (let i = 0; i < newSelectors.length; i++) {
            selector = newSelectors[i];
            value = opr.contentFilterPrivate.isElementBlocked(selector);
            this.selectors_.set(selector, value);
            if (value) {
                return true;
            }
        }
    }

    hideElement_(element) {
        if (!element) {
            return;
        }

        if (element.style && element.style.getPropertyValue('display') !== 'none') {
            element.style.setProperty('display', 'none', 'important');
        }
        this.filteredElements_.add(element);
    }

    initialize_() {
        this.root_ = document.body;
        document.head.appendChild(this.styles_);
        this.observer_ = new MutationObserver(
            mutations => setTimeout(() => this.onDocumentChange_(mutations), 0));
        this.observer_.observe(this.root_, {
            attributes: true,
            attributeOldValue: true,
            attributeFilter: ['class', 'id'],  // 'style'
            childList: true,
            subtree: true,
        });

        this.root_.addEventListener(
            'error', e => this.onResourceError_(e.target), true);
        opr.contentFilterPrivate.onRulesLoaded.addListener(
            () => this.onRulesLoaded_());

        this.filter_();
    }

    isUrlFiltered_(url, callback) {
        let isBlocked = this.urls_.get(url);
        if (isBlocked === undefined) {
            isBlocked = opr.contentFilterPrivate.isURLBlocked(url);
            this.urls_.set(url, isBlocked);
        }

        return isBlocked;
    }

    onDocumentChange_(mutations) {
        for (let record of mutations) {
            let addedNodes = record.addedNodes;
            if (addedNodes) {
                for (let i = 0; i < addedNodes.length; i++) {
                    if (!this.filterElementIfNeeded_(addedNodes[i])) {
                        this.filterChildrenIfNeeded_(addedNodes[i]);
                    }
                }
            }

            if (record.target) {
                this.filterElementIfNeeded_(record.target);
            }
        }
    }

    onResourceError_(element) {
        if (element && element.src && this.isUrlFiltered_(element.src)) {
            this.hideElement_(element);
        }
    }

    onRulesLoaded_() {
        this.filteredElements_ = new WeakSet();
        this.selectors_ = new Map();
        this.urls_ = new Map();
        this.applySelectors_().then(() => this.filter_());
    }

    whenDomReady_() {
        return new Promise(resolve => {
            if (document.readyState !== 'loading') {
                resolve();
            } else {
                document.addEventListener('DOMContentLoaded', () => resolve());
            }
        });
    }
}

new FilterContent();