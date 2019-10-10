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

	module.exports = __webpack_require__(870);


/***/ }),

/***/ 835:
/***/ (function(module, exports) {

	module.exports = function() { throw new Error("define cannot be used indirect"); };


/***/ }),

/***/ 865:
/***/ (function(module, exports) {

	module.exports = require("./kendo.datepicker");

/***/ }),

/***/ 870:
/***/ (function(module, exports, __webpack_require__) {

	var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;(function(f, define){
	    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [ __webpack_require__(865), __webpack_require__(871), __webpack_require__(872), __webpack_require__(873) ], __WEBPACK_AMD_DEFINE_FACTORY__ = (f), __WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ? (__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
	})(function(){

	var __meta__ = { // jshint ignore:line
	    id: "editable",
	    name: "Editable",
	    category: "framework",
	    depends: [ "datepicker", "numerictextbox", "validator", "binder" ],
	    hidden: true
	};

	/* jshint eqnull: true */
	(function($, undefined) {
	    var kendo = window.kendo,
	        ui = kendo.ui,
	        Widget = ui.Widget,
	        extend = $.extend,
	        oldIE = kendo.support.browser.msie && kendo.support.browser.version < 9,
	        isFunction = kendo.isFunction,
	        isPlainObject = $.isPlainObject,
	        inArray = $.inArray,
	        POINT = ".",
	        support = kendo.support,
	        AUTOCOMPLETEVALUE = support.browser.chrome ? "disabled" : "off",
	        nameSpecialCharRegExp = /("|\%|'|\[|\]|\$|\.|\,|\:|\;|\+|\*|\&|\!|\#|\(|\)|<|>|\=|\?|\@|\^|\{|\}|\~|\/|\||`)/g,
	        ERRORTEMPLATE = '<div class="k-widget k-tooltip k-tooltip-validation" style="margin:0.5em"><span class="k-icon k-i-warning"> </span>' +
	                    '#=message#<div class="k-callout k-callout-n"></div></div>',
	        CHANGE = "change";
	    var EQUAL_SET = "equalSet";
	    var specialRules = ["url", "email", "number", "date", "boolean"];

	    function fieldType(field) {
	        field = field != null ? field : "";
	        return field.type || $.type(field) || "string";
	    }

	    function convertToValueBinding(container) {
	        container.find(":input:not(:button, .k-combobox .k-input, [" + kendo.attr("role") + "=listbox], [" + kendo.attr("role") + "=upload], [" + kendo.attr("skip") + "], [type=file])").each(function() {
	            var bindAttr = kendo.attr("bind"),
	                binding = this.getAttribute(bindAttr) || "",
	                bindingName = this.type === "checkbox" || this.type === "radio" ? "checked:" : "value:",
	                fieldName = this.name;

	            if (binding.indexOf(bindingName) === -1 && fieldName) {
	                binding += (binding.length ? "," : "") + bindingName + fieldName;

	                $(this).attr(bindAttr, binding);
	            }
	        });
	    }

	    function createAttributes(options) {
	        var field = (options.model.fields || options.model)[options.field],
	            type = fieldType(field),
	            validation = field ? field.validation : {},
	            ruleName,
	            DATATYPE = kendo.attr("type"),
	            BINDING = kendo.attr("bind"),
	            rule,
	            attr = {
	                name: options.field,
	                title: options.title ? options.title : options.field
	            };

	        for (ruleName in validation) {
	            rule = validation[ruleName];

	            if (inArray(ruleName, specialRules) >= 0) {
	                attr[DATATYPE] = ruleName;
	            } else if (!isFunction(rule)) {
	                var culture = kendo.getCulture();

	                if (typeof rule === "number" && culture.name.length) {
	                    var numberFormat = culture.numberFormat;
	                    var stringRule = rule.toString()
	                        .replace(POINT, numberFormat[POINT]);

	                    attr[ruleName] = stringRule;
	                } else {
	                    attr[ruleName] = isPlainObject(rule) ? rule.value || ruleName : rule;
	                }
	            }

	            attr[kendo.attr(ruleName + "-msg")] = rule.message;

	            attr.autocomplete = AUTOCOMPLETEVALUE;
	        }

	        if (inArray(type, specialRules) >= 0) {
	            attr[DATATYPE] = type;
	        }

	        attr[BINDING] = (type === "boolean" ? "checked:" : "value:") + options.field;

	        return attr;
	    }

	    function addIdAttribute(container, attr) {
	        var id = container.attr("id");

	        if (id) {
	            attr.id = id;
	            container.removeAttr("id");
	        }

	        return attr;
	    }

	    function convertItems(items) {
	        var idx,
	            length,
	            item,
	            value,
	            text,
	            result;

	        if (items && items.length) {
	            result = [];
	            for (idx = 0, length = items.length; idx < length; idx++) {
	                item = items[idx];
	                text = item.text || item.value || item;
	                value = item.value == null ? (item.text || item) : item.value;

	                result[idx] = { text: text, value: value };
	            }
	        }
	        return result;
	    }

	    var editors = {
	        "number": function(container, options) {
	            var attr = createAttributes(options);
	            $('<input type="text"/>').attr(attr).appendTo(container).kendoNumericTextBox({ format: options.format });
	            $('<span ' + kendo.attr("for") + '="' + options.field + '" class="k-invalid-msg"/>').hide().appendTo(container);
	        },
	        "date": function(container, options) {
	            var attr = createAttributes(options),
	                format = options.format;

	            if (format) {
	                format = kendo._extractFormat(format);
	            }

	            attr[kendo.attr("format")] = format;

	            $('<input type="text"/>').attr(attr).appendTo(container).kendoDatePicker({ format: options.format });
	            $('<span ' + kendo.attr("for") + '="' + options.field + '" class="k-invalid-msg"/>').hide().appendTo(container);
	        },
	        "string": function(container, options) {
	            var attr = createAttributes(options);

	            $('<input type="text" class="k-textbox"/>').attr(attr).appendTo(container);
	        },
	        "boolean": function(container, options) {
	            var attr = createAttributes(options);
	            $('<input type="checkbox" />').attr(attr).appendTo(container);
	        },
	        "values": function(container, options) {
	            var attr = createAttributes(options);
	            var items = kendo.stringify(convertItems(options.values));
	            $('<select ' + kendo.attr("text-field") + '="text"' + kendo.attr("value-field") + '="value"' +
	                kendo.attr("source") + "=\'" + (items ? items.replace(/\'/g,"&apos;") : items) +
	                "\'" + kendo.attr("role") + '="dropdownlist"/>') .attr(attr).appendTo(container);
	            $('<span ' + kendo.attr("for") + '="' + options.field + '" class="k-invalid-msg"/>').hide().appendTo(container);
	        }
	    };

	    var mobileEditors = {
	        "number": function (container, options) {
	            var attr = createAttributes(options);
	            attr = addIdAttribute(container, attr);

	            $('<input type="number"/>').attr(attr).appendTo(container);
	        },
	        "date": function (container, options) {
	            var attr = createAttributes(options);
	            attr = addIdAttribute(container, attr);

	            $('<input type="date"/>').attr(attr).appendTo(container);
	        },
	        "string": function (container, options) {
	            var attr = createAttributes(options);
	            attr = addIdAttribute(container, attr);

	            $('<input type="text" />').attr(attr).appendTo(container);
	        },
	        "boolean": function (container, options) {
	            var attr = createAttributes(options);
	            attr = addIdAttribute(container, attr);

	            $('<input type="checkbox" />').attr(attr).appendTo(container);
	        },
	        "values": function (container, options) {
	            var attr = createAttributes(options);
	            var items = options.values;
	            var select = $('<select />');

	            attr = addIdAttribute(container, attr);

	            for (var index in items) {
	                $('<option value="' + items[index].value + '">' + items[index].text + '</option>').appendTo(select);
	            }

	            select.attr(attr).appendTo(container);
	        }
	    };

	    function addValidationRules(modelField, rules) {
	        var validation = modelField ? (modelField.validation || {}) : {},
	            rule,
	            descriptor;

	        for (rule in validation) {
	            descriptor = validation[rule];

	            if (isPlainObject(descriptor) && descriptor.value) {
	                descriptor = descriptor.value;
	            }

	            if (isFunction(descriptor)) {
	                rules[rule] = descriptor;
	            }
	        }
	    }

	    var Editable = Widget.extend({
	        init: function(element, options) {
	            var that = this;

	            if (options.target) {
	                options.$angular = options.target.options.$angular;

	                if (options.target.pane) {
	                    that._isMobile = true;
	                }
	            }
	            Widget.fn.init.call(that, element, options);
	            that._validateProxy = $.proxy(that._validate, that);
	            that.refresh();
	        },

	        events: [CHANGE],

	        options: {
	            name: "Editable",
	            editors: editors,
	            mobileEditors: mobileEditors,
	            clearContainer: true,
	            errorTemplate: ERRORTEMPLATE,
	            skipFocus: false
	        },

	        editor: function(field, modelField) {
	            var that = this,
	                editors = that._isMobile ? mobileEditors : that.options.editors,
	                isObject = isPlainObject(field),
	                fieldName = isObject ? field.field : field,
	                model = that.options.model || {},
	                isValuesEditor = isObject && field.values,
	                type = isValuesEditor ? "values" : fieldType(modelField),
	                isCustomEditor = isObject && field.editor,
	                editor = isCustomEditor ? field.editor : editors[type],
	                container = that.element.find("[" + kendo.attr("container-for") + "=" + fieldName.replace(nameSpecialCharRegExp, "\\$1")+ "]");

	            editor = editor ? editor : editors.string;

	            if (isCustomEditor && typeof field.editor === "string") {
	                editor = function(container) {
	                    container.append(field.editor);
	                };
	            }

	            container = container.length ? container : that.element;
	            editor(container, extend(true, {}, isObject ? field : { field: fieldName }, { model: model }));
	        },

	        _validate: function(e) {
	            var that = this,
	                input,
	                value = e.value,
	                preventChangeTrigger = that._validationEventInProgress,
	                values = {},
	                bindAttribute = kendo.attr("bind"),
	                fieldName = e.field.replace(nameSpecialCharRegExp, "\\$1"),
	                bindingRegex = new RegExp("(value|checked)\\s*:\\s*" + fieldName + "\\s*(,|$)");

	            values[e.field] = e.value;

	            input = $(':input[' + bindAttribute + '*="' + fieldName + '"]', that.element)
	                .filter("[" + kendo.attr("validate") + "!='false']").filter(function() {
	                   return bindingRegex.test($(this).attr(bindAttribute));
	                });
	            if (input.length > 1) {
	                input = input.filter(function () {
	                    var element = $(this);
	                    return !element.is(":radio") || element.val() == value;
	                });
	            }

	            try {
	                that._validationEventInProgress = true;

	                if (!that.validatable.validateInput(input) || (!preventChangeTrigger && that.trigger(CHANGE, { values: values }))) {
	                    e.preventDefault();
	                }

	            } finally {
	                that._validationEventInProgress = false;
	            }
	        },

	        end: function() {
	            return this.validatable.validate();
	        },

	        destroy: function() {
	            var that = this;

	            that.angular("cleanup", function(){
	                return { elements: that.element };
	            });

	            Widget.fn.destroy.call(that);

	            that.options.model.unbind("set", that._validateProxy);
	            that.options.model.unbind(EQUAL_SET, that._validateProxy);

	            kendo.unbind(that.element);

	            if (that.validatable) {
	                that.validatable.destroy();
	            }
	            kendo.destroy(that.element);

	            that.element.removeData("kendoValidator");

	            if (that.element.is("[" + kendo.attr("role") + "=editable]")) {
	                that.element.removeAttr(kendo.attr("role"));
	            }
	        },

	        refresh: function() {
	            var that = this,
	                idx,
	                length,
	                fields = that.options.fields || [],
	                container = that.options.clearContainer ? that.element.empty() : that.element,
	                model = that.options.model || {},
	                rules = {},
	                field,
	                isObject,
	                fieldName,
	                modelField,
	                modelFields;

	            if (!$.isArray(fields)) {
	                fields = [fields];
	            }

	            for (idx = 0, length = fields.length; idx < length; idx++) {
	                 field = fields[idx];
	                 isObject = isPlainObject(field);
	                 fieldName = isObject ? field.field : field;
	                 modelField = (model.fields || model)[fieldName];

	                 addValidationRules(modelField, rules);

	                 that.editor(field, modelField);
	            }

	            if (that.options.target) {
	                that.angular("compile", function(){
	                    return {
	                        elements: container,
	                        data: container.map(function() { return { dataItem: model }; })
	                    };
	                });
	            }

	            if (!length) {
	                modelFields = model.fields || model;
	                for (fieldName in modelFields) {
	                    addValidationRules(modelFields[fieldName], rules);
	               }
	            }

	            convertToValueBinding(container);

	            if (that.validatable) {
	                that.validatable.destroy();
	            }

	            kendo.bind(container, that.options.model);

	            that.options.model.unbind("set", that._validateProxy);
	            that.options.model.bind("set", that._validateProxy);

	            that.options.model.unbind(EQUAL_SET, that._validateProxy);
	            that.options.model.bind(EQUAL_SET, that._validateProxy);

	            that.validatable = new kendo.ui.Validator(container, {
	                validateOnBlur: false,
	                errorTemplate: that.options.errorTemplate || undefined,
	                rules: rules });

	            if (!that.options.skipFocus) {
	                var focusable = container.find(":kendoFocusable").eq(0).focus();
	                if (oldIE) {
	                    focusable.focus();
	                }
	            }
	        }
	   });

	   ui.plugin(Editable);
	})(window.kendo.jQuery);

	return window.kendo;

	}, __webpack_require__(835));


/***/ }),

/***/ 871:
/***/ (function(module, exports) {

	module.exports = require("./kendo.numerictextbox");

/***/ }),

/***/ 872:
/***/ (function(module, exports) {

	module.exports = require("./kendo.validator");

/***/ }),

/***/ 873:
/***/ (function(module, exports) {

	module.exports = require("./kendo.binder");

/***/ })

/******/ });