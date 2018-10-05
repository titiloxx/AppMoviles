function informacionDormis(fechax) {

    var serviceURL = '/Home/InformacionModal';
        $.ajax({
            type: "POST",
            url: serviceURL,
            data: param = { fecha: fechax},
            success: successFuncx,
            error: errorFuncx
        });

    }

function successFuncx(datax) {
    $('#myModal').modal('show');
    $("#infoModal").html(datax);
}

function errorFuncx() {
    alert('Hubo un error prosesando la solicitud');
}