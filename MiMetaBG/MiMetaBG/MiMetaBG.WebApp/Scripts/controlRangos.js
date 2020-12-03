function RefreshTotal() {

    slider_output1.value = dreamSaveRange.value;
    slider_output2.value = dreamTimeRange.value;

    calculo();
}

function cambioAhorro() {
    $("#slider_output1").text(formatearNumero($("#dreamSaveRange").val()));
    calculo();
}

function cambioTiempo() {
    $("#slider_output2").text($("#dreamTimeRange").val());
    calculo();
}

function calculo() {
    var montoAhorro = document.getElementById("dreamSaveRange").value;
    var montoTiempo = document.getElementById("dreamTimeRange").value;

    document.getElementById("TotalAhorro").innerText = "$ " + formatearNumero(montoAhorro * montoTiempo);
}

function validarNum(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    patron = /\d/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function formatearNumero(monto) {
    var valor = new Intl.NumberFormat().format(monto);
    var str = valor;
    var res = str.replace(",", ".");
    return res;
}

function Characters(event) {
    //apóstrofe, paréntesis abierto, paréntesis cierre, asterisco, signo más, coma, dos puntos, punto y coma, doble comilla, comilla simple
    if (event.which == 39 || event.which == 40 || event.which == 41 || event.which == 42 || event.which == 43 || event.which == 44 || event.which == 58 || event.which == 59 || event.which == 34 || event.which == 39) {
        event.returnValue = false;
    }
}
 
function CharactersAlfaNum(event) {
    if ((event.which < 48 ) || (event.which > 57 && event.which < 65) || (event.which > 90 && event.which < 97) || (event.which > 122 )) {
        event.returnValue = false;
    }
}

function validaNumericos(event) {
    if (event.charCode >= 48 && event.charCode <= 57) {
        return true;
    }
    return false;
}

window.oncontextmenu = function() {
    return false;
}

function llenarCancelacion(requestData)
{
    var datos = requestData.split('|');
    document.getElementById("idCuenta").textContent = datos[11];
    var data = datos[0] + '|' + datos[1] + '|' + datos[2] + '|' + datos[3] + '|' + datos[4] + '|' + datos[6] + '|' + datos[7] + '|' + datos[8] + '|' + datos[9] + '|' + datos[10] + '|' + datos[5];
    localStorage.setItem("data", data);
}

function llenarExtra(requestData)
{
    var datos = requestData.split('|');
    document.getElementById("lblNombreE").textContent = datos[10];
    var s = datos[10];
    var data = datos[0] + '|' + datos[1] + '|' + datos[2] + '|' + datos[3];
    localStorage.setItem("dataE", data);
}

function AgregarSuenio()
{
    var data = localStorage.getItem("dataE");
    var datos = data.split('|');
    
    var monto = $("#agregarMonto").val()
    if ($("#agregarMonto").val() != "") {
        
        //agregarMonto
        $(".cartprocess").fadeTo(3500, 0.50,
       function () 
       {
           var dataJson = { 'jsonRequest': datos[0] + "|" + datos[1] + "|" + $("#listasM").val() + "|" + $("#agregarMonto").val() };

            $.ajax({
                type: 'POST',
                url: '/Metas/Metas/ExtraSuenio',
                data: dataJson,
                dataType: 'JSON',
                async: false,
                success: function (data) {
                    
                    if (data[0] == "0001") {
                        $("#mensajerror").text(data[1]);
                        $("#error").modal('show');
                        $('.cartprocess').hide();
                    } else if (data == "0000")
                        location.reload();
                    else if (data == "0100") {
                        $('.cartprocess').hide();
                        window.location.replace('/Metas/Error/PageSesion')
                    } else {
                        $('.cartprocess').hide();
                        window.location.replace('/Metas/Error/PageError')
                    }
                },
            
                error: function () {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageError')
                }
            });
       });
    } else {
        document.getElementById('msgAlertaM').style.display = 'block';
    }

}

function CancelarSuenio() {
    $(".cartprocess").fadeTo(3500, 0.50,
       function () 
   {
        var data = localStorage.getItem("data");
        var datos = data.split('|');
            
        var dataJson = {
            'jsonRequest': datos[0] + "|" + datos[1] + "|" + datos[3] + "|" + datos[4] + "|" + datos[5] + "|" + datos[6] + "|" +
                datos[7] + "|" + datos[8] + "|" + datos[10]
        };

        $.ajax({
            type: 'POST',
            url: '/Metas/Metas/CancelarSuenio',
            data: dataJson,
            dataType: 'JSON',
            async: false,
            success: function (data) {
                if (data[0] == "0001") {
                    $("#mensajerror").text(data[1]);
                    $("#error").modal('show');
                    $('.cartprocess').hide();
                } else if (data == "0000")
                    location.reload();
                else if (data == "0100") {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageSesion')
                } else {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageError')
                }
            },
        
            error: function () {
                window.location.replace('/Metas/Error/PageError')
                $('.cartprocess').hide();
            }
        });
    });
}

function llenaData(cuentaAhorro, numSolicitud, tipoCuenta, ctaDebito, montoDebito, frecuencia, dia1, dia2, montoMeta, fechaMeta, nombreSuenio, valorDebAcumulado, cuotaMeta, fechaEmision) {

    localStorage.setItem("montoA", valorDebAcumulado);
    localStorage.setItem("cuentaA", cuentaAhorro);
    localStorage.setItem("numSolicitud", numSolicitud);
    localStorage.setItem("tipoCuenta", tipoCuenta);
    localStorage.setItem("frecuencia", frecuencia);
    localStorage.setItem("fechaMeta", fechaMeta);

    fechaEmision = fechaEmision.split(" ");
    localStorage.setItem("fechaEmision", fechaEmision[0].replace("/", "-").replace("/", "-"));

    document.getElementById("spMonto").textContent = "$" + montoMeta;
    document.getElementById("spMensual").textContent = "$" + montoDebito;
    document.getElementById("lblNombre").textContent = nombreSuenio;

    if (fechaMeta == "") {
        $("#divFecha").css("display", "none");
        $("#divMonto").css("display", "block");
    } else {
        $("#divMonto").css("display", "none");
        $("#divFecha").css("display", "block");
    }

    $("#txtMontoMeta").val(montoMeta);
    $("#cuentasAC").val(ctaDebito) // + "|" + tipoCuenta);

    fechaMeta = fechaMeta.split("-");

    $("#diaMensual").val(dia1);
    $("#diaQuincena").val(dia2);

    $("#plazoEdit").val(getFechaMes(fechaMeta[1]));
    $("#AnioEdit").val(fechaMeta[0]);

    var fecha = $("#AnioEdit").val() + "-" + getFechaNumber($("#plazoEdit").val()) + "-" + $("#diaMensual").val();
    var f = new Date(fecha);

    var input = document.getElementById("dreamSaveRangeEdit");
    input.value = cuotaMeta;

    if (frecuencia == "M") {
        $("#divQuincenal").css("display", "none");
    } else {
        $("#divQuincenal").css("display", "block");
    }
}

function cambioAhorroConsultaSuenio() {
    $("#slider_output1").text(formatearNumero($("#dreamSaveRange").val()));
    calculoConsultaSuenio();
}

function RefreshTotalConsultaSuenio() {

    document.getElementById("spMensual").textContent = "$" + dreamSaveRangeEdit.value;

    calFecha();
}

function calculoConsultaSuenio() {

    document.getElementById("spMensual").innerText = "$" + document.getElementById("dreamSaveRangeEdit").value;
}

function calFecha() {
    if (localStorage.getItem("fechaMeta") != "") {

        var fecha = $("#AnioEdit").val() + "-" + getFechaNumber($("#plazoEdit").val()) + "-" + $("#diaMensual").val();
        var f = new Date(fecha);
        var fechaEmision = localStorage.getItem("fechaEmision");
        var fA = new Date();

        var fechaMeses = "";

        
        var oneDay = 24 * 60 * 60 * 1000;
        var diffDays = Math.round(Math.abs((f.getTime() - fA.getTime()) / (oneDay)));
        if (localStorage.getItem("frecuencia") == "Q") {
            fechaMeses = parseInt(diffDays / 15);
        } else {
            fechaMeses = parseInt(diffDays / 30);
        }


        var input = document.getElementById("dreamSaveRangeEdit");
        var total = (input.value * fechaMeses) + (parseInt(localStorage.getItem("montoA")));

        document.getElementById("spMonto").textContent = "$" + formatearNumero(total);
        $("#txtMontoMeta").val(formatearNumero(total));
    }
}

function getFechaNumber() {
    var anioLetras = $("#plazoEdit").val();
    var anio = 0;

    if (anioLetras == "Enero")
        anio = "01";
    else if (anioLetras == "Febrero")
        anio = "02";
    else if (anioLetras == "Marzo")
        anio = "03";
    else if (anioLetras == "Abril")
        anio = "04";
    else if (anioLetras == "Mayo")
        anio = "05";
    else if (anioLetras == "Junio")
        anio = "06";
    else if (anioLetras == "Julio")
        anio = "07";
    else if (anioLetras == "Agosto")
        anio = "08";
    else if (anioLetras == "Septiembre")
        anio = "09";
    else if (anioLetras == "Octubre")
        anio = "10";
    else if (anioLetras == "Noviembre")
        anio = "11";
    else if (anioLetras == "Diciembre")
        anio = "12";

    return anio;
}

function sendDataEditar(dataJson) {
    var dia2 = "";
    $(".cartprocess").fadeTo(3500, 0.50,
        function () 
   { 
        if (localStorage.getItem("frecuencia") == "Q") {
            dia2 = $("#diaQuincena").val();
        } else {
            dia2 = "0";
        }

        var fecha = "";
        if (localStorage.getItem("fechaMeta") != "") {
            fecha = $("#AnioEdit").val() + "-" + getFechaNumber($("#plazoEdit").val()) + "-" + $("#diaMensual").val();
        }

        var request = localStorage.getItem("cuentaA") + "|" + localStorage.getItem("numSolicitud") + "|" + $("#cuentasAC").val() + "|" +
            document.getElementById('spMensual').textContent.replace("$", "") + "|" + $("#diaMensual").val() + "|" + dia2 + "|" + $("#txtMontoMeta").val().replace("$", "")
            + "|" + fecha + "|" + localStorage.getItem("frecuencia");

            var dataJson = { 'jsonRequest': request };

        $.ajax({
            type: 'POST',
            url: '/Metas/Metas/EditarSuenio',
            data: dataJson,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data[0] == "0001") {
                    $("#mensajerror").text(data[1]);
                    $("#error").modal('show');
                    $(".cartprocess").hide();
                } else if (data == "0000") {
                    $(".cartprocess").hide();
                    location.reload();
                }
                else if (data == "0100") {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageSesion')
                } else {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageError')
                }
            },
            error: function () {
                window.location.replace('/Metas/Error/PageError')
                $(".cartprocess").hide();
            }
        });
    });
}

function CreaSuenio()
{
    $(".cartprocess").fadeTo(3500, 0.50,
         function ()
    {
        var fechaMetaMeta = localStorage.getItem("FechaFinalDebito");
   
        var monthNames = [
        "Enero", "Febrero", "Marzo",
        "Abril", "Mayo", "Junio", "Julio",
        "Agosto", "Septiembre", "Octubre",
        "Noviembre", "Diciembre"
        ];

        var fecha = new Date();
        var dias = fechaMetaMeta * 30;

        fecha.setDate(fecha.getDate() + dias);
        var f_dia = fecha.getDate();
        var f_mes = fecha.getMonth();
        var f_anio = fecha.getFullYear();
        f_mes = monthNames[f_mes];
        var fechaFinalMeta = f_dia + "/" + f_mes + "/" + f_anio;

        var dataJson = {
            'RequestJson': $("#cuentasAC").val() + "|" + $("#cuentasA").val() + "|" + localStorage.getItem("MontoDebito") + "|" +
            $("#listasM").val() + "|" + fechaFinalMeta + "|" + localStorage.getItem("NombreMeta") + "|" + localStorage.getItem("IdSegmento") + "|" +
            localStorage.getItem("MontoTotal")
        };             

        $.ajax({
            type: 'POST',
            url: '/Metas/Metas/CrearSuenio',
            data: dataJson,
            dataType: 'JSON',
            async: false,
            success: function (data) {

                if (data[0] == "0001") {
                    $("#mensajerror").text(data[1]);
                    $("#error").modal('show');
                    $('.cartprocess').hide();
                } else if (data == "0000")
                {
                    sessionStorage.removeItem("NombreMeta");
                    sessionStorage.removeItem("MontoTotal");
                    sessionStorage.removeItem("MontoDebito");
                    sessionStorage.removeItem("FechaFinalDebito");
                    sessionStorage.removeItem("IdSegmento");
                    sessionStorage.removeItem("cliente");
                    //sessionStorage.removeItem
                    $(".cartprocess").hide();
                    $("#successModal").modal('show');
                }
                else if (data == "0100") {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageSesion')
                } else {
                    $('.cartprocess').hide();
                    window.location.replace('/Metas/Error/PageError')
                }
            },
            error: function () {
                window.location.replace('/Metas/Error/PageError')
                $('.cartprocess').hide();
            }
        });
     });
}

function getFechaMes(mes) {
    var anio = "";

    if (mes == "01")
        anio = "Enero";
    else if (mes == "02")
        anio = "Febrero";
    else if (mes == "03")
        anio = "Marzo";
    else if (mes == "04")
        anio = "Abril";
    else if (mes == "05")
        anio = "Mayo";
    else if (mes == "06")
        anio = "Junio";
    else if (mes == "07")
        anio = "Julio";
    else if (mes == "08")
        anio = "Agosto";
    else if (mes == "09")
        anio = "Septiembre";
    else if (mes == "10")
        anio = "Octubre";
    else if (mes == "11")
        anio = "Noviembre";
    else if (mes == "12")
        anio = "Diciembre";

    return anio;
}

function cierra() {
    $("#error").modal('hide');
}

function atras(page) {
    if (page == "Consulta") {

    }
}

function ActualizarVentas(ventas)
{
    var dataJson = {
        'VecesMostradas': ventas
    };

    var urlTC = $("#idTC").val();
    var urlMC = $("#idMC").val();

    $.ajax({
        type: 'POST',
        url: '/Metas/Metas/ActualizarVtaCruzada',
        data: dataJson,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data == "0000") {
                if (ventas == 1) {
                    window.open(urlTC);
                    location.reload();
                } else if (ventas == 2) {
                    window.open(urlMC);
                    location.reload();
                } else
                    location.reload();
            }
            else if (data == "0100") {
                window.location.replace('/Metas/Error/PageSesion')
            } else {
                window.location.replace('/Metas/Error/PageError')
            }
        },

        error: function () {
            window.location.replace('/Metas/Error/PageError')
        }
    });    
}

function ShowProgress() {
    $(".cartprocess").fadeTo(30, 0.40, function () { });
    return false;
}

function HideProgress() {
    $('.cartprocess').hide();
}












