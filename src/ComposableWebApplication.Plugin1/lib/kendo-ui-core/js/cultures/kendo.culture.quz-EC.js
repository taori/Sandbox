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

	__webpack_require__(619);
	module.exports = __webpack_require__(619);


/***/ }),

/***/ 619:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["quz-EC"] = {
	        name: "quz-EC",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": ".",
	            ".": ",",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n %","n %"],
	                decimals: 2,
	                ",": ".",
	                ".": ",",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "US Dollar",
	                abbr: "USD",
	                pattern: ["$ -n","$ n"],
	                decimals: 2,
	                ",": ".",
	                ".": ",",
	                groupSize: [3],
	                symbol: "$"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["inti","awaki","wanra","chillay","kullka","chaska","wakma"],
	                    namesAbbr: ["int","awk","wan","chy","kuk","cha","wak"],
	                    namesShort: ["in","aw","wn","cy","ku","ck","wk"]
	                },
	                months: {
	                    names: ["kulla","panchi","pawkar","ayriwa","aymuray","raymi","sitwa","karwa","kuski","wayru","sasi","kapak"],
	                    namesAbbr: ["kull","pan","paw","ayr","aym","ray","sit","kar","kus","way","sas","kap"]
	                },
	                AM: [""],
	                PM: [""],
	                patterns: {
	                    d: "dd/MM/yyyy",
	                    D: "dddd, dd' de 'MMMM' de 'yyyy",
	                    F: "dddd, dd' de 'MMMM' de 'yyyy H:mm:ss",
	                    g: "dd/MM/yyyy H:mm",
	                    G: "dd/MM/yyyy H:mm:ss",
	                    m: "d 'de' MMMM",
	                    M: "d 'de' MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "H:mm",
	                    T: "H:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM' de 'yyyy",
	                    Y: "MMMM' de 'yyyy"
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