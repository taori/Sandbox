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

	__webpack_require__(434);
	module.exports = __webpack_require__(434);


/***/ }),

/***/ 434:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["kk"] = {
	        name: "kk",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": " ",
	            ".": ",",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 2,
	                ",": " ",
	                ".": ",",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "",
	                abbr: "",
	                pattern: ["-n $","n $"],
	                decimals: 2,
	                ",": " ",
	                ".": ",",
	                groupSize: [3],
	                symbol: "₸"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["жексенбі","дүйсенбі","сейсенбі","сәрсенбі","бейсенбі","жұма","сенбі"],
	                    namesAbbr: ["Жс","Дс","Сс","Ср","Бс","Жм","Сб"],
	                    namesShort: ["Жс","Дс","Сс","Ср","Бс","Жм","Сб"]
	                },
	                months: {
	                    names: ["Қаңтар","Ақпан","Наурыз","Сәуір","Мамыр","Маусым","Шілде","Тамыз","Қыркүйек","Қазан","Қараша","Желтоқсан"],
	                    namesAbbr: ["Қаң.","Ақп.","Нау.","Сәу.","Мам.","Мау.","Шіл.","Там.","Қыр.","Қаз.","Қар.","Жел."]
	                },
	                AM: ["таңғы","таңғы","ТАҢҒЫ"],
	                PM: ["түскі/кешкі","түскі/кешкі","ТҮСКІ/КЕШКІ"],
	                patterns: {
	                    d: "dd.MM.yyyy",
	                    D: "yyyy 'ж'. d MMMM, dddd",
	                    F: "yyyy 'ж'. d MMMM, dddd HH:mm:ss",
	                    g: "dd.MM.yyyy HH:mm",
	                    G: "dd.MM.yyyy HH:mm:ss",
	                    m: "d MMMM",
	                    M: "d MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "HH:mm",
	                    T: "HH:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "yyyy 'ж'. MMMM",
	                    Y: "yyyy 'ж'. MMMM"
	                },
	                "/": ".",
	                ":": ":",
	                firstDay: 1
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });