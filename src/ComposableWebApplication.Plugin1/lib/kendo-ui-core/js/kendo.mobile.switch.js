module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ({

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

	module.exports = __webpack_require__(906);


/***/ }),

/***/ 835:
/***/ (function(module, exports) {

	module.exports = function() { throw new Error("define cannot be used indirect"); };


/***/ }),

/***/ 850:
/***/ (function(module, exports) {

	module.exports = require("./kendo.userevents");

/***/ }),

/***/ 902:
/***/ (function(module, exports) {

	module.exports = require("./kendo.fx");

/***/ }),

/***/ 906:
/***/ (function(module, exports, __webpack_require__) {

	var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;/* jshint multistr: true */
	(function(f, define){
	    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [ __webpack_require__(902), __webpack_require__(850) ], __WEBPACK_AMD_DEFINE_FACTORY__ = (f), __WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ? (__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
	})(function(){

	var __meta__ = { // jshint ignore:line
	    id: "mobile.switch",
	    name: "Switch",
	    category: "mobile",
	    description: "The mobile Switch widget is used to display two exclusive choices.",
	    depends: [ "fx", "userevents" ]
	};

	(function($, undefined) {
	    var kendo = window.kendo,
	        ui = kendo.mobile.ui,
	        outerWidth = kendo._outerWidth,
	        Widget = ui.Widget,
	        support = kendo.support,
	        CHANGE = "change",
	        SWITCHON = "switch-on",
	        SWITCHOFF = "switch-off",
	        MARGINLEFT = "margin-left",
	        ACTIVE_STATE = "state-active",
	        DISABLED_STATE = "state-disabled",
	        DISABLED = "disabled",
	        RESOLVEDPREFIX = support.transitions.css === undefined ? "" : support.transitions.css,
	        TRANSFORMSTYLE = RESOLVEDPREFIX + "transform",
	        proxy = $.proxy;

	    function className(name) {
	        return "km-" + name;
	    }

	    function limitValue(value, minLimit, maxLimit) {
	        return Math.max(minLimit, Math.min(maxLimit, value));
	    }

	    var SWITCH_MARKUP = '<span class="' + className("switch") + ' ' + className("widget") + '">\
	        <span class="' + className("switch-wrapper") + '">\
	            <span class="' + className("switch-background") + '"></span>\
	        </span> \
	        <span class="' + className("switch-container") + '">\
	            <span class="' + className("switch-handle") + '"> \
	                <span class="' + className("switch-label-on") + '">{0}</span> \
	                <span class="' + className("switch-label-off") + '">{1}</span> \
	            </span> \
	        </span>\
	    </span>';

	    var Switch = Widget.extend({
	        init: function(element, options) {
	            var that = this, checked;

	            Widget.fn.init.call(that, element, options);

	            options = that.options;

	            that.wrapper = $(kendo.format(SWITCH_MARKUP, options.onLabel, options.offLabel));
	            that.handle = that.wrapper.find(".km-switch-handle");
	            that.background = that.wrapper.find(".km-switch-background");
	            that.wrapper.insertBefore(that.element).prepend(that.element);

	            that._drag();

	            that.origin = parseInt(that.background.css(MARGINLEFT), 10);

	            that.constrain = 0;
	            that.snapPoint = 0;

	            element = that.element[0];
	            element.type = "checkbox";
	            that._animateBackground = true;

	            checked = that.options.checked;

	            if (checked === null) {
	                checked = element.checked;
	            }

	            that.check(checked);

	            that.options.enable = that.options.enable && !that.element.attr(DISABLED);
	            that.enable(that.options.enable);

	            that.refresh();
	            kendo.notify(that, kendo.mobile.ui);
	        },

	        refresh: function() {
	            var that = this,
	                handleWidth = outerWidth(that.handle, true);

	            that.width = that.wrapper.width();

	            that.constrain  = that.width - handleWidth;
	            that.snapPoint = that.constrain / 2;

	            if (typeof that.origin != "number") {
	                that.origin = parseInt(that.background.css(MARGINLEFT), 10);
	            }

	            that.background.data("origin", that.origin);

	            that.check(that.element[0].checked);
	        },

	        events: [
	            CHANGE
	        ],

	        options: {
	            name: "Switch",
	            onLabel: "on",
	            offLabel: "off",
	            checked: null,
	            enable: true
	        },

	        check: function(check) {
	            var that = this,
	                element = that.element[0];

	            if (check === undefined) {
	                return element.checked;
	            }

	            that._position(check ? that.constrain : 0);
	            element.checked = check;
	            that.wrapper
	                .toggleClass(className(SWITCHON), check)
	                .toggleClass(className(SWITCHOFF), !check);
	        },

	        // alias for check, NG support
	        value: function() {
	            return this.check.apply(this, arguments);
	        },

	        destroy: function() {
	            Widget.fn.destroy.call(this);
	            this.userEvents.destroy();
	        },

	        toggle: function() {
	            var that = this;

	            that.check(!that.element[0].checked);
	        },

	        enable: function(enable) {
	            var element = this.element,
	                wrapper = this.wrapper;

	            if(typeof enable == "undefined") {
	                enable = true;
	            }

	            this.options.enable = enable;

	            if(enable) {
	                element.removeAttr(DISABLED);
	            } else {
	                element.attr(DISABLED, DISABLED);
	            }

	            wrapper.toggleClass(className(DISABLED_STATE), !enable);
	        },

	        _resize: function() {
	            this.refresh();
	        },

	        _move: function(e) {
	            var that = this;
	            e.preventDefault();
	            that._position(limitValue(that.position + e.x.delta, 0, that.width - outerWidth(that.handle, true)));
	        },

	        _position: function(position) {
	            var that = this;

	            that.position = position;
	            that.handle.css(TRANSFORMSTYLE, "translatex(" + position + "px)");

	            if (that._animateBackground) {
	                that.background.css(MARGINLEFT, that.origin + position);
	            }
	        },

	        _start: function() {
	            if(!this.options.enable) {
	                this.userEvents.cancel();
	            } else {
	                this.userEvents.capture();
	                this.handle.addClass(className(ACTIVE_STATE));
	            }
	        },

	        _stop: function() {
	            var that = this;

	            that.handle.removeClass(className(ACTIVE_STATE));
	            that._toggle(that.position > that.snapPoint);
	        },

	        _toggle: function (checked) {
	            var that = this,
	                handle = that.handle,
	                element = that.element[0],
	                value = element.checked,
	                duration = kendo.mobile.application && kendo.mobile.application.os.wp ? 100 : 200,
	                distance;

	            that.wrapper
	                .toggleClass(className(SWITCHON), checked)
	                .toggleClass(className(SWITCHOFF), !checked);

	            that.position = distance = checked * that.constrain;

	            if (that._animateBackground) {
	                that.background
	                    .kendoStop(true, true)
	                    .kendoAnimate({ effects: "slideMargin", offset: distance, reset: true, reverse: !checked, axis: "left", duration: duration });
	            }

	            handle
	                .kendoStop(true, true)
	                .kendoAnimate({
	                    effects: "slideTo",
	                    duration: duration,
	                    offset: distance + "px,0",
	                    reset: true,
	                    complete: function () {
	                        if (value !== checked) {
	                            element.checked = checked;
	                            that.trigger(CHANGE, { checked: checked });
	                        }
	                    }
	                });
	        },

	        _drag: function() {
	            var that = this;

	            that.userEvents = new kendo.UserEvents(that.wrapper, {
	                fastTap: true,
	                tap: function() {
	                    if(that.options.enable) {
	                        that._toggle(!that.element[0].checked);
	                    }
	                },
	                start: proxy(that._start, that),
	                move: proxy(that._move, that),
	                end: proxy(that._stop, that)
	            });
	        }
	    });

	    ui.plugin(Switch);
	})(window.kendo.jQuery);

	return window.kendo;

	}, __webpack_require__(835));


/***/ })

/******/ });