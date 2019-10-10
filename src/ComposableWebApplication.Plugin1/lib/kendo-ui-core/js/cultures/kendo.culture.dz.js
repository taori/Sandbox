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

	__webpack_require__(132);
	module.exports = __webpack_require__(132);


/***/ }),

/***/ 132:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["dz"] = {
	        name: "dz",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": ",",
	            ".": ".",
	            groupSize: [3,2],
	            percent: {
	                pattern: ["-n %","n %"],
	                decimals: 2,
	                ",": ",",
	                ".": ".",
	                groupSize: [3,2],
	                symbol: "%"
	            },
	            currency: {
	                name: "",
	                abbr: "",
	                pattern: ["-$n","$n"],
	                decimals: 2,
	                ",": ",",
	                ".": ".",
	                groupSize: [3,2],
	                symbol: "Nu."
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["གཟའ་ཟླ་བ་","གཟའ་མིག་དམར་","གཟའ་ལྷག་པ་","གཟའ་ཕུར་བུ་","གཟའ་པ་སངས་","གཟའ་སྤེན་པ་","གཟའ་ཉི་མ་"],
	                    namesAbbr: ["ཟླ་","མིར་","ལྷག་","ཕུར་","སངས་","སྤེན་","ཉི་"],
	                    namesShort: ["ཟླ་","མིར་","ལྷག་","ཕུར་","སངས་","སྤེན་","ཉི་"]
	                },
	                months: {
	                    names: ["སྤྱི་ཟླ་དངཔ་","སྤྱི་ཟླ་གཉིས་པ་","སྤྱི་ཟླ་གསུམ་པ་","སྤྱི་ཟླ་བཞི་པ","སྤྱི་ཟླ་ལྔ་པ་","སྤྱི་ཟླ་དྲུག་པ","སྤྱི་ཟླ་བདུན་པ་","སྤྱི་ཟླ་བརྒྱད་པ་","སྤྱི་ཟླ་དགུ་པ་","སྤྱི་ཟླ་བཅུ་པ་","སྤྱི་ཟླ་བཅུ་གཅིག་པ་","སྤྱི་ཟླ་བཅུ་གཉིས་པ་"],
	                    namesAbbr: ["ཟླ་༡","ཟླ་༢","ཟླ་༣","ཟླ་༤","ཟླ་༥","ཟླ་༦","ཟླ་༧","ཟླ་༨","ཟླ་༩","ཟླ་༡༠","ཟླ་༡༡","ཟླ་༡༢"]
	                },
	                AM: ["AM","am","AM"],
	                PM: ["PM","pm","PM"],
	                patterns: {
	                    d: "yyyy-MM-dd",
	                    D: "dddd, སྤྱི་ལོ་yyyy MMMM ཚེས་dd",
	                    F: "dddd, སྤྱི་ལོ་yyyy MMMM ཚེས་dd ཆུ་ཚོད་h:mm:ss tt",
	                    g: "yyyy-MM-dd ཆུ་ཚོད་ h སྐར་མ་ mm tt",
	                    G: "yyyy-MM-dd ཆུ་ཚོད་h:mm:ss tt",
	                    m: "MMMM d",
	                    M: "MMMM d",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "ཆུ་ཚོད་ h སྐར་མ་ mm tt",
	                    T: "ཆུ་ཚོད་h:mm:ss tt",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "yyyy MMMM",
	                    Y: "yyyy MMMM"
	                },
	                "/": "-",
	                ":": ":",
	                firstDay: 0
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });