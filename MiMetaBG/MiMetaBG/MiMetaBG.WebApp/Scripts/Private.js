function CampoBlancoUser() {
    var value = document.getElementById("usuario").value;
    value = value.trim();
    if (value.length == 0) {
        event.preventDefault();
    }
}

function SalvarDatos(IdSegmento, nombre, descripcion, codimagen, umbral, estado, producto, prioridad, mensaje_seg, cod_host)
{
    document.getElementById("opcion").value = "modificar";
    document.getElementById("IdSegmento").value = IdSegmento;
    document.getElementById("nombre").value = nombre;
    document.getElementById("descripcion").value = descripcion;
    document.getElementById("codimagen").value = codimagen;
    document.getElementById("umbral").value = umbral;
    document.getElementById("estado").value = estado;
    document.getElementById("producto").value = producto;
    document.getElementById("prioridad").value = prioridad;
    document.getElementById("mensaje_seg").value = mensaje_seg;
    document.getElementById("cod_host").value = cod_host;

    document.myform.submit();  
}

function SalvarDatosImg(codimagen, imagen, emojin) {
    document.getElementById("opcion").value = "modificar";
    document.getElementById("CodImagen").value = codimagen;

    document.myform.submit();
}

function CamposBlancoImg() {
    var fileimagen = document.getElementById('fileimagen').value;
    var fileemoji = document.getElementById('fileemoji').value;

    if (fileimagen.length == 0) {
        alert("Debe leccionar una imagen");
        return false;
    }
    if (fileemoji.length == 0) {
        alert("Debe seleccionar imagen emoji");
        return false;
    }
}

function CamposBlancoSeg() {
    var nombre = document.getElementById('nombre').value;
    var descripcion = document.getElementById('descripcion').value;
    var codimagen = document.getElementById('codimagen').value;
    var umbral = document.getElementById('umbral').value;
    var estado = document.getElementById('estado').value;
    var producto = document.getElementById('producto').value;
    var prioridad = document.getElementById('prioridad').value;
    var mensaje_seg = document.getElementById('mensaje_seg').value;
    var cod_host = document.getElementById('cod_host').value;

    if (nombre.length == 0) {
        alert("Debe ingresar el nombre del segmento");
        return false;
    }
    if (descripcion.length == 0) {
        alert("Debe ingresar la descripcion del segmento");
        return false;
    }
    if (codimagen.length == 0) {
        alert("Debe ingresar codigo de la imagen del segmento");
        return false;
    }
    if (estado.length == 0) {
        alert("Debe seleccionar el estado del segmento");
        return false;
    }
    if (producto.length == 0) {
        alert("Debe ingresar el producto del segmento");
        return false;
    }
    if (prioridad.length == 0) {
        alert("Debe ingresar la prioridad del segmento");
        return false;
    }
    if (mensaje_seg.length == 0) {
        alert("Debe ingresar el segmento del segmento");
        return false;
    }
    if (cod_host.length == 0) {
        alert("Debe ingresar el codigo de host");
        return false;
    }

}

function SalvarDatosCI(codimagen) {
    sessionStorage.setItem('codimagen', codimagen);
    history.back();
}

function SalvarDatosProd(codproducto) {
    sessionStorage.setItem('codproducto', codproducto);
    history.back();
}

function SalvarDatosSP(IdSegmento, nombre, descripcion, codimagen, umbral, estado, producto, prioridad, mensaje_seg, cod_host)
{
    document.getElementById("opcion").value = "modificar";
    document.getElementById("IdSegmento").value = IdSegmento;
    document.getElementById("nombre").value = nombre;
    document.getElementById("descripcion").value = descripcion;
    document.getElementById("codimagen").value = codimagen;
    document.getElementById("umbral").value = umbral;
    document.getElementById("estado").value = estado;
    document.getElementById("producto").value = producto;
    document.getElementById("prioridad").value = prioridad;
    document.getElementById("mensaje_seg").value = mensaje_seg;
    document.getElementById("cod_host").value = cod_host;

    document.myform.submit();
}