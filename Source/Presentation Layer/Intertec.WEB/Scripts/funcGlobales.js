
function CrearDialogo(nombreDIV, alto, ancho, titulo) {
    $("#" + nombreDIV).dialog({
        autoOpen: false,
        resizable: false,
        height: alto,
        width: ancho,
        title: titulo,
        modal: true

    });
}


function CrearDialogoEliminar(nombreDIV, alto, ancho, titulo) {
    $("#" + nombreDIV).dialog({
        autoOpen: false,
        resizable: false,
        height: alto,
        width: ancho,
        title: titulo,
        modal: true,
        open: function (event, ui) {
            var valorID = $(this).data('id');
            $('#hdEliminarID').val(valorID);
        },
        //        buttons: {
        //            'Sí': function () {
        //                //var dd = $($(this).data("id")).val();
        //                var dd = $(this).data('id');
        //                Eliminar(dd);
        //                $(this).dialog('close');
        //            },
        //            'Cerrar': function () {
        //                $(this).dialog('close');
        //            }
        //        },
        close: function (ev, ui) {
        }
    });
}

function MostrarMensaje(nombreDIV) {
    $('#' + nombreDIV).dialog("open");
}

function MostrarMensajeEliminar(nombreDIV, idEliminar) {
    $('#' + nombreDIV).data("id", idEliminar).dialog("open");
}

function MostrarMensajeTextArea(nombreDIV, maxlength) {
    $("#spanCaracteres").text(maxlength);
    $('#' + nombreDIV).dialog("open");
}


function CerrarDialogo(nombreDIV) {
    $('#' + nombreDIV).dialog("close");
}

function CerrarDialogoC(nombreDIV, url) {
    $('#' + nombreDIV).dialog("close");
    $(location).attr('href', url);
}

function EliminarRegistro(url, parameters, async) {
    //debugger;
    //alert(parameters);
    var rsp = '';
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async,
        responseType: "json",
        data: JSON.stringify(parameters),
        success: function (response) {
            rsp = response.d;
        },
        failure: function (msg) {
            rsp = -1;
        },
        error: function (request, status, error) {
            alert(jQuery.parseJSON(request.responseText).Message);
        }
    });
    return rsp;
}

function DesactivarValidacion() {
    Page_ValidationActive = false;
}


function vacio(cadena)  
  {                                   
    var blanco = " \n\t" + String.fromCharCode(13); 
                                       
    var i;                             
    var es_vacio;                      
    for(i = 0, es_vacio = true; (i < cadena.length) && es_vacio; i++)  
      es_vacio = blanco.indexOf(cadena.charAt(i)) != - 1;  
    return(es_vacio);  
  }

  function rblSelectedValue(radiolistbtn) {
      var cont=0;
      var selectedvalue;
      var radiobtn = radiolistbtn.getElementsByTagName("input");   
      var radioLength = radiobtn.length;
      for (var i= 0; i < radioLength; i++) {
          if (radiobtn[i].checked) {
              //selectedvalue = radio[i].value;
              //alert(selectedvalue);
              return false;
              break;
          }
          cont++;
      }

      if (cont == radioLength)
          return true;
      else
          return false;
  }

//valida cantidad max de caracteres permitidos en un textarea.
function Count(text, maxlength) {
    if (text.value.length > maxlength) {
        text.value = text.value.substring(0, maxlength);
        MostrarMensajeTextArea('msjAlertaCaracteres', maxlength);
       // alert(" Solo se deben ingresar " + maxlength + " caracteres");
    }
}


function validarChr(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /^[A-Z a-zñÑáéíóúÁÉÍÓÚ]*$/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

//function validarDecimal(e) {
//    tecla = (document.all) ? e.keyCode : e.which;
//    if (tecla == 8) return true;
//    patron = /^\-?[0-9,]{0,}\.?\d{0,}$/;
//    te = String.fromCharCode(tecla);
//    return patron.test(te);
//}


function validarNro(e) {
    var key;
    key = (document.all) ? e.keyCode : e.which;

    if (key < 48 || key > 57) {
        if (key == 8) // backspace (retroceso)
        { return true; }
        else
        { return false; }
    }
    return true;
}

// JS NUMERO DECINAL CON MONTOS MAXIMOS Y CANTIDAD DE CARACTERES
function jsEsNumeroDecimal(evento) {
    key = evento.keyCode;
    return (key >= 48 && key <= 57) || (key == 46);
}

// Formatea un numero a numero Decimales y valida que el monto no sea mayor al tope
function formatearNumeroWithTope(source, decimales, tope) {
    var retorno;

    if (source.value == "") return "0.00";
    if (validaTopeNumerico(source, tope)) {
        if (decimales > 0) retorno = "0.00";
        else retorno = "0";
        if ((source.value != '') && (source.value > 0)) {
            retorno = MontoNumeroDecimal(source.value, decimales);
        }
    }
    else {
        retorno = "0.00";
    }
    source.value = retorno;
}
// Valida un Tope
function validaTopeNumerico(source, tope) {
    if (source.value == "") return true;
    var montoTemp = parseFloat(source.value);
    var montoTope = parseFloat(tope);
    if (montoTemp > montoTope) {
        return false;
    }
    return true;
}

// Ejcuta el Formato un numero a numero Decimales
function MontoNumeroDecimal(
	montoInput,
	pifMaxDecimalPlaces
) {
    var montoRetorno;
    var sNumero;
    var iPosicion;
    var sCaracter;
    var sNumeroEntero = '';
    var sNumeroDecimal = '';
    var sNumeroEnteroReverse;
    var sNumeroEnteroReverseNew = '';
    var valNumero = /^\-?[0-9,]{0,}\.?\d{0,}$/;

    var iLongitudDecimal;
    var bConSigno = false;

    sNumero = montoInput;

    if (valNumero.test(sNumero)) {
        var montoTemp = parseFloat(sNumero);
        sNumero = montoTemp.toString();
        iPosicion = sNumero.indexOf('.');

        // Separo Decimales y Enteros
        if (iPosicion >= 0) {
            sNumeroEntero = sNumero.substring(0, iPosicion);
            sNumeroDecimal = sNumero.substring(iPosicion + 1, sNumero.length);
            iLongitudDecimal = sNumeroDecimal.length;
        }
        else {
            sNumeroEntero = sNumero;
            iLongitudDecimal = 0;
        }

        // Evaluo parte Decimal
        if (pifMaxDecimalPlaces > 0) {
            // Si la longitud de decimales es mayor
            if (iLongitudDecimal > pifMaxDecimalPlaces) {
                sNumeroDecimal = sNumeroDecimal;

                sNumeroDecimal = sNumeroDecimal.substring(0, pifMaxDecimalPlaces) + '.' + sNumeroDecimal.substring(pifMaxDecimalPlaces, sNumeroDecimal.length);

                sNumeroDecimal = Math.round(sNumeroDecimal);
            }
            // Agrego los Ceros que falten en la parte decimal
            if (iLongitudDecimal < pifMaxDecimalPlaces) {
                for (iPosicion = 1; iPosicion <= (pifMaxDecimalPlaces - iLongitudDecimal); iPosicion++) {
                    sNumeroDecimal = sNumeroDecimal + '0';
                }
            }
            sNumeroDecimal = '.' + sNumeroDecimal;
        }
        //Obtengo numero completo
        sNumero = sNumeroEntero + sNumeroDecimal;
        montoRetorno = sNumero;
    }
    return montoRetorno;
}



// JS Fechas
var MONTH_NAMES = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
var DAY_NAMES = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');
function LZ(x) { return (x < 0 || x > 9 ? "" : "0") + x }

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------
function isDate(val, format) {
    var date = getDateFromFormat(val, format);
    if (date == 0) { return false; }
    return true;
}



// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date, format) {
    format = format + "";
    var result = "";
    var i_format = 0;
    var c = "";
    var token = "";
    var y = date.getYear() + "";
    var M = date.getMonth() + 1;
    var d = date.getDate();
    var E = date.getDay();
    var H = date.getHours();
    var m = date.getMinutes();
    var s = date.getSeconds();
    var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
    // Convert real date parts into formatted versions
    var value = new Object();
    if (y.length < 4) { y = "" + (y - 0 + 1900); }
    value["y"] = "" + y;
    value["yyyy"] = y;
    value["yy"] = y.substring(2, 4);
    value["M"] = M;
    value["MM"] = LZ(M);
    value["MMM"] = MONTH_NAMES[M - 1];
    value["NNN"] = MONTH_NAMES[M + 11];
    value["d"] = d;
    value["dd"] = LZ(d);
    value["E"] = DAY_NAMES[E + 7];
    value["EE"] = DAY_NAMES[E];
    value["H"] = H;
    value["HH"] = LZ(H);
    if (H == 0) { value["h"] = 12; }
    else if (H > 12) { value["h"] = H - 12; }
    else { value["h"] = H; }
    value["hh"] = LZ(value["h"]);
    if (H > 11) { value["K"] = H - 12; } else { value["K"] = H; }
    value["k"] = H + 1;
    value["KK"] = LZ(value["K"]);
    value["kk"] = LZ(value["k"]);
    if (H > 11) { value["a"] = "PM"; }
    else { value["a"] = "AM"; }
    value["m"] = m;
    value["mm"] = LZ(m);
    value["s"] = s;
    value["ss"] = LZ(s);
    while (i_format < format.length) {
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        if (value[token] != null) { result = result + value[token]; }
        else { result = result + token; }
    }
    return result;
}

// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
    var digits = "1234567890";
    for (var i = 0; i < val.length; i++) {
        if (digits.indexOf(val.charAt(i)) == -1) { return false; }
    }
    return true;
}
function _getInt(str, i, minlength, maxlength) {
    for (var x = maxlength; x >= minlength; x--) {
        var token = str.substring(i, i + x);
        if (token.length < minlength) { return null; }
        if (_isInteger(token)) { return token; }
    }
    return null;
}

// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val, format) {
    val = val + "";
    format = format + "";
    var i_val = 0;
    var i_format = 0;
    var c = "";
    var token = "";
    var token2 = "";
    var x, y;
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = 1;
    var hh = now.getHours();
    var mm = now.getMinutes();
    var ss = now.getSeconds();
    var ampm = "";

    while (i_format < format.length) {
        // Get next token from format string
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == "yyyy" || token == "yy" || token == "y") {
            if (token == "yyyy") { x = 4; y = 4; }
            if (token == "yy") { x = 2; y = 2; }
            if (token == "y") { x = 2; y = 4; }
            year = _getInt(val, i_val, x, y);
            if (year == null) { return 0; }
            i_val += year.length;
            if (year.length == 2) {
                if (year > 70) { year = 1900 + (year - 0); }
                else { year = 2000 + (year - 0); }
            }
        }
        else if (token == "MMM" || token == "NNN") {
            month = 0;
            for (var i = 0; i < MONTH_NAMES.length; i++) {
                var month_name = MONTH_NAMES[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                    if (token == "MMM" || (token == "NNN" && i > 11)) {
                        month = i + 1;
                        if (month > 12) { month -= 12; }
                        i_val += month_name.length;
                        break;
                    }
                }
            }
            if ((month < 1) || (month > 12)) { return 0; }
        }
        else if (token == "EE" || token == "E") {
            for (var i = 0; i < DAY_NAMES.length; i++) {
                var day_name = DAY_NAMES[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == "MM" || token == "M") {
            month = _getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12)) { return 0; }
            i_val += month.length;
        }
        else if (token == "dd" || token == "d") {
            date = _getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31)) { return 0; }
            i_val += date.length;
        }
        else if (token == "hh" || token == "h") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "HH" || token == "H") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "KK" || token == "K") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "kk" || token == "k") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
            i_val += hh.length; hh--;
        }
        else if (token == "mm" || token == "m") {
            mm = _getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
            i_val += mm.length;
        }
        else if (token == "ss" || token == "s") {
            ss = _getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
            i_val += ss.length;
        }
        else if (token == "a") {
            if (val.substring(i_val, i_val + 2).toLowerCase() == "am") { ampm = "AM"; }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm") { ampm = "PM"; }
            else { return 0; }
            i_val += 2;
        }
        else {
            if (val.substring(i_val, i_val + token.length) != token) { return 0; }
            else { i_val += token.length; }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length) { return 0; }
    // Is date valid for month?
    if (month == 2) {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
            if (date > 29) { return 0; }
        }
        else { if (date > 28) { return 0; } }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
        if (date > 30) { return 0; }
    }
    // Correct hours value
    if (hh < 12 && ampm == "PM") { hh = hh - 0 + 12; }
    else if (hh > 11 && ampm == "AM") { hh -= 12; }
    var newdate = new Date(year, month - 1, date, hh, mm, ss);
    return newdate.getTime();
}

// ------------------------------------------------------------------
// parseDate( date_string [, prefer_euro_format] )
//
// This function takes a date string and tries to match it to a
// number of possible date formats to get the value. It will try to
// match against the following international formats, in this order:
// y-M-d   MMM d, y   MMM d,y   y-MMM-d   d-MMM-y  MMM d
// M/d/y   M-d-y      M.d.y     MMM-d     M/d      M-d
// d/M/y   d-M-y      d.M.y     d-MMM     d/M      d-M
// A second argument may be passed to instruct the method to search
// for formats like d/M/y (european format) before M/d/y (American).
// Returns a Date object or null if no patterns match.
// ------------------------------------------------------------------
function parseDate(val) {
    var preferEuro = (arguments.length == 2) ? arguments[1] : false;
    generalFormats = new Array('y-M-d', 'MMM d, y', 'MMM d,y', 'y-MMM-d', 'd-MMM-y', 'MMM d');
    monthFirst = new Array('M/d/y', 'M-d-y', 'M.d.y', 'MMM-d', 'M/d', 'M-d');
    dateFirst = new Array('d/M/y', 'd-M-y', 'd.M.y', 'd-MMM', 'd/M', 'd-M');
    var checkList = new Array('generalFormats', preferEuro ? 'dateFirst' : 'monthFirst', preferEuro ? 'monthFirst' : 'dateFirst');
    var d = null;
    for (var i = 0; i < checkList.length; i++) {
        var l = window[checkList[i]];
        for (var j = 0; j < l.length; j++) {
            d = getDateFromFormat(val, l[j]);
            if (d != 0) { return new Date(d); }
        }
    }
    return null;
}



//var oAlert = alert;
//function alert(txt, title) {
//    var message = '';
//    if (txt instanceof Object) {
//        jQuery.each(txt, function (i, item) {
//            message += item + '<br />';
//        });
//    }
//    else {
//        message = txt;
//    }
//    try {
//        jAlert(message, title);
//    } catch (e) {
//        oAlert(message);
//    }
//}