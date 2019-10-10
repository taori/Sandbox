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

	__webpack_require__(409);
	module.exports = __webpack_require__(409);


/***/ }),

/***/ 409:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["ja"] = {
	        name: "ja",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": ",",
	            ".": ".",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 2,
	                ",": ",",
	                ".": ".",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "",
	                abbr: "",
	                pattern: ["-$n","$n"],
	                decimals: 0,
	                ",": ",",
	                ".": ".",
	                groupSize: [3],
	                symbol: "¥"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["日曜日","月曜日","火曜日","水曜日","木曜日","金曜日","土曜日"],
	                    namesAbbr: ["日","月","火","水","木","金","土"],
	                    namesShort: ["日","月","火","水","木","金","土"]
	                },
	                months: {
	                    names: ["1月","2月","3月","4月","5月","6月","7月","8月","9月","10月","11月","12月"],
	                    namesAbbr: ["1","2","3","4","5","6","7","8","9","10","11","12"]
	                },
	                AM: ["午前","午前","午前"],
	                PM: ["午後","午後","午後"],
	                patterns: {
	                    d: "yyyy/MM/dd",
	                    D: "yyyy'年'M'月'd'日'",
	                    F: "yyyy'年'M'月'd'日' H:mm:ss",
	                    g: "yyyy/MM/dd H:mm",
	                    G: "yyyy/MM/dd H:mm:ss",
	                    m: "M月d日",
	                    M: "M月d日",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "H:mm",
	                    T: "H:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "yyyy'年'M'月'",
	                    Y: "yyyy'年'M'月'"
	                },
	                "/": "/",
	                ":": ":",
	                firstDay: 0
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });