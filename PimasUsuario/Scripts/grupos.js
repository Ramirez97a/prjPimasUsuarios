

$(document).ready(function () {

    function showLoader() {
        document.getElementById('overlay').style.display = 'block';
        document.getElementById('loader').style.display = 'block';
    }

    function hideLoader() {
        document.getElementById('overlay').style.display = 'none';
        document.getElementById('loader').style.display = 'none';
    }

    showLoader();

    setTimeout(function () {
        hideLoader();
        var contengrupoElement = document.getElementById('contengrupo');
        contengrupoElement.removeAttribute('style');
        obtenerYEnviarTematicaIds();
    }, 2000);



    $('#filtros').on('click', function (event) {
        event.preventDefault();
        var idTematica = document.getElementById('idfantastaSubtematica').value;

        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');
        obtenerDatosGetByTematicYAgregarElemento(idTematica);

    });
    $('#filtrosAll1').on('click', function (event) {
        $('.filtro').prop('checked', false);
        let filtro = 0;
        let vertodos = true;
        $('#carImagen3').css('background-color', '');
        obtenerDatosYAgregarElemento(filtro, vertodos);

    });

    $(document).on('click', '#filtrosAll', function () {
        event.preventDefault();
        let filtro = 0;
        let vertodos = true;
        var elements = document.querySelectorAll('.show');
        var tematicaElement = document.getElementById('tematicatxt');
        var lblhidden = document.querySelectorAll('[id*="lblhidden"]');

        $('.filtro').prop('checked', false);       
        obtenerDatosYAgregarElemento(filtro, vertodos);

        elements.forEach(function (element) {
            element.classList.remove('show');
        });
        tematicaElement.setAttribute('aria-expanded', 'false');

        // Itera sobre los elementos y elimina el atributo style
        lblhidden.forEach(function (element) {
            element.style.display = "none";
        });
        $('#carImagen1').css('background-color', ''); $('#carImagen2').css('background-color', '');  
        $('#carImagen3').css('background-color', ''); $('#carImagen4').css('background-color', '');  
        $('#carImagen5').css('background-color', ''); $('#carImagen6').css('background-color', '');
        $('#carImagen7').css('background-color', ''); $('#carImagen8').css('background-color', '');  
        
    })
    $(document).on('click', '#vertodos', function () {
        event.preventDefault();
        let filtro = 0;
        let vertodos = true;
        var elements = document.querySelectorAll('.show');
        var tematicaElement = document.getElementById('tematicatxt');
        var lblhidden = document.querySelectorAll('[id*="lblhidden"]');

        $('.filtro').prop('checked', false);
        obtenerDatosYAgregarElemento(filtro, vertodos);

        elements.forEach(function (element) {
            element.classList.remove('show');
        });
        tematicaElement.setAttribute('aria-expanded', 'false');

        // Itera sobre los elementos y elimina el atributo style
        lblhidden.forEach(function (element) {
            element.style.display = "none";
        });
        $('#carImagen1').css('background-color', ''); $('#carImagen2').css('background-color', '');
        $('#carImagen3').css('background-color', ''); $('#carImagen4').css('background-color', '');
        $('#carImagen5').css('background-color', ''); $('#carImagen6').css('background-color', '');
        $('#carImagen7').css('background-color', ''); $('#carImagen8').css('background-color', '');

    })

    $(document).on('click', '#subtematica', function () {
        event.preventDefault();
    })
    $(document).on('click', '#VerGrupos', function () {

        window.location.href = `/group/Index?`;

    })

   

    $(document).on('click', '#tematicatxt', function () {
        var elements = document.querySelectorAll('[id*="lblhidden"]');

        // Itera sobre los elementos y elimina el atributo style
        elements.forEach(function (element) {
            element.removeAttribute("style");
        });
    })

    $(document).on('click', '#carImg1', function () {

        if ($('#f1').is(':checked')) {
            $('#carImagen1').css('background-color', '#0E1726');    
        } else {
            $('#carImagen1').css('background-color', '');    
        }        
    })
    $(document).on('click', '#carImg2', function () {

        if ($('#f2').is(':checked')) {
            $('#carImagen2').css('background-color', '#0E1726');
        } else {
            $('#carImagen2').css('background-color', '');
        }
    })
    $(document).on('click', '#carImg3', function () {

        if ($('#f3').is(':checked')) {
            $('#carImagen3').css('background-color', '#0E1726');
        } else {
            $('#carImagen3').css('background-color', '');
        }
    })
    $(document).on('click', '#carImg4', function () {

        if ($('#f4').is(':checked')) {
            $('#carImagen4').css('background-color', '#0E1726');
        } else {
            $('#carImagen4').css('background-color', '');
        }
    })
    $(document).on('click', '#carImg5', function () {

        if ($('#f5').is(':checked')) {
            $('#carImagen5').css('background-color', '#0E1726');
        } else {
            $('#carImagen5').css('background-color', '');
        }
    })
    $(document).on('click', '#carImg6', function () {

        if ($('#f6').is(':checked')) {
            $('#carImagen6').css('background-color', '#0E1726');
        } else {
            $('#carImagen6').css('background-color', '');
        }
    })
    $(document).on('click', '#carImg7', function () {

        if ($('#f7').is(':checked')) {
            $('#carImagen7').css('background-color', '#0E1726');
        } else {
            $('#carImagen7').css('background-color', '');
        }
    })
    $(document).on('click', '#carImg9', function () {

        if ($('#f8').is(':checked')) {
            $('#carImagen8').css('background-color', '#0E1726');
        } else {
            $('#carImagen8').css('background-color', '');
        }
    })

       
});


function obtenerDatosGetByTematicYAgregarElemento(idTematica) {
    var idGrupo = window.location.pathname.split('/').pop();

    var url = `/api/Assets/byTematic?tematicId=${idTematica}&group=${idGrupo}`;
    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            let filtro = 0;
            let vertodos = true;

            agregarAssest(data, filtro, vertodos);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}


function obtenerDatosYAgregarElemento(idfiltro, vertodos) {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Assets/getByGroupLow?id=${idGrupo}`;


    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            console.log("esta es la data");
            console.log(data);
            console.log(" fin esta es la data");
            agregarAssest(data, idfiltro, vertodos);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}



function obtenerDatosSubtematicasYAgregarElemento() {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Group/tematicas?id=${idGrupo}`;

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            console.log("agregarElementosTematicas: " +data);
            agregarElementosTematicas(data);
            /* agregarAssest(data);*/
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

function agregarElementosTematicas(data) {
    const ul = document.getElementById('accordionExample');
    ul.innerHTML = ''; // Limpiar el contenido actual

    const contenidoInicial = `
        <li class="menu active ">
            <a href="#" class="dropdown-toggle" id="btnhome">
                <div>
                    <img src="/Content/imagenes/Logo_inicio.png" alt="logo" width="30px" height="30px" style="margin-right:40px;"><span>PIMAS</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="tematicaid" id="VerGrupos">
                <div>
                    <span>Grupos</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="todosArchivos" id="vertodos">
                <div>
                    <span>Todos los Archivos</span>
                </div>
            </a>
        </li>
    `;
    ul.innerHTML += contenidoInicial;

    const tematicasAgrupadas = {};

    data.Data.forEach((item) => {
        const { Id, Nombre, Hijos, Nietos } = item;

        if (Nombre && Hijos.length > 0) {
            tematicasAgrupadas[Id] = tematicasAgrupadas[Id] || [];
            tematicasAgrupadas[Id].push({ Nombre, Hijos, Nietos });
        }
    });

    for (const [tematicaId, tematicasArray] of Object.entries(tematicasAgrupadas)) {
        const nuevoLi = document.createElement('li');
        nuevoLi.className = '';

        const hijosUnicos = {};
        const nietosUnicos = {};

        nuevoLi.innerHTML = `
            <input id="group-${tematicaId}" type="checkbox" hidden />
            <label for="group-${tematicaId}" class="tematicatxt" id="tematicatxt" data-tematica-id-item="${tematicaId}" value="${tematicasArray[0]?.Nombre}">
                <span class="fa fa-angle-right"></span>${tematicasArray[0]?.Nombre || ''}
            </label>

            <ul class="group-list">
                ${tematicasArray[0]?.Hijos.filter(subtema => {
            if (!hijosUnicos[subtema.TematicaID]) {
                hijosUnicos[subtema.TematicaID] = true;
                return true;
            }
            return false;
        }).map(subtema => {
            const tieneNietos = tematicasArray[0]?.Nietos.some(nieto => nieto.ParentTematicaID === subtema.TematicaID);

            return tieneNietos ? `
                        <li>
                            <input id="sub-group-${subtema.TematicaID}" type="checkbox" hidden />
                            <label for="sub-group-${subtema.TematicaID}" data-tematica-id-item="${subtema.TematicaID}" value="${subtema.NombreTematica}" id="SubtematicaHijos" style="background-color: red;">
                                <span class="fa fa-angle-right"></span>${subtema.NombreTematica}
                            </label>
                            <ul class="sub-group-list">
                                ${tematicasArray[0]?.Nietos.filter(nieto => {
                if (nieto.ParentTematicaID === subtema.TematicaID && !nietosUnicos[nieto.TematicaID]) {
                    nietosUnicos[nieto.TematicaID] = true;
                    return true;
                }
                return false;
            }).map(nieto => `
                                    <li>
                                        <a href="#" data-tematica-id-item="${nieto.TematicaID}">${nieto.NombreTematica}</a>
                                    </li>
                                `).join('')}
                            </ul>
                        </li>
                    ` : `
                        <li>
                            <label for="sub-group-${subtema.TematicaID}" data-tematica-id-item="${subtema.TematicaID}" value="${subtema.NombreTematica}" id="SubtematicaHijos">
                                <span></span>${subtema.NombreTematica}
                            </label>
                        </li>`;
        }).join('')}
            </ul>
        `;

        ul.appendChild(nuevoLi);
    }

    obtenerdatosnav();
}

function obtenerYEnviarTematicaIds() {
    const labels = document.querySelectorAll('.tematicatxt');
    const tematicaIDs = [];

    labels.forEach(element => {
        const tematicaID = element.getAttribute('data-tematica-id-item');
        tematicaIDs.push(tematicaID);
    });

    
    tematicaIDs.forEach(tematicaID => {
        enviarATuAPI(tematicaID);
    });
}

function enviarATuAPI(tematicaIDs) {
    console.log(tematicaIDs);
    var url = `/api/Group/getcolorbyID?tematicaId=${tematicaIDs}`;
   
    $.ajax({
        url: url,
        type: 'GET',
        contentType: 'application/json',  
        data: JSON.stringify({ tematicaId: tematicaIDs }),  
        success: function (data) {
            console.log("prueba data" + data);
            cambiarColor(data);
        },
        error: function (xhr, status, error) {
            console.error('Error al enviar datos DEL COLOR:', xhr.responseText);
        }
    });
}
function cambiarColor(data) {
    if (!data || !data.Data) {
        console.error('No se encontró Data en la respuesta:', data);
        return;
    }

    const labels = document.querySelectorAll('.tematicatxt');
    labels.forEach(element => {
        const tematicaID = element.getAttribute('data-tematica-id-item');

       
        if (data.Data.TematicaID && data.Data.TematicaID.toString() === tematicaID) {
         
            element.style.backgroundColor = '#' + data.Data.CodColor;
        }
    });
}



function agregarAssest(data, idfiltro, vertodos = false) {
    console.log(idfiltro);
    let imagenUrl;


    imagenUrl = "/Content/imagenes/Theory-and-Exercises.png";
    const cardContainer = document.getElementById("cardContairnerAssest");
    cardContainer.innerHTML = "";

    if (idfiltro && idfiltro.length > 0) {

        // Aseguramos que idFiltros sea un array de números
        const idFiltrosNumeros = idfiltro.map(id => parseInt(id));

        const asset = data.Data.filter(asset => idFiltrosNumeros.includes(asset.TipoAssetID));
        console.log("assets:", asset);

        if (asset)
        {
            asset.forEach(asset => {
                let imagenUrl;
                if (asset.TipoAssetID === 1) {
                    imagenUrl = "/Content/imagenes/TeoríayEjercicios.png";
                }
                else if (asset.TipoAssetID === 2) {
                    imagenUrl = "/Content/imagenes/Solucionesdeloejercicios.png";

                }
                else if (asset.TipoAssetID === 3) {
                    imagenUrl = "/Content/imagenes/Actividades.png";

                }
                else if (asset.TipoAssetID === 4) {
                    imagenUrl = "/Content/imagenes/RespuestasdelasActividades.png";

                }
                else if (asset.TipoAssetID === 5) {
                    imagenUrl = "/Content/imagenes/Resumen.png";

                }
                else if (asset.TipoAssetID === 6) {
                    imagenUrl = "/Content/imagenes/EjerciciosAdicionales.png";

                }
                else if (asset.TipoAssetID === 7) {
                    imagenUrl = "/Content/imagenes/Autoevaluación.png";

                }
                else if (asset.TipoAssetID === 9) {
                    imagenUrl = "/Content/imagenes/Profundización";

                }
                const card = document.createElement("div");
                card.className = "col mb-4";
                card.id = "cardContenedor_";
                card.setAttribute("data-value", `${asset.TipoAssetID}`);

                // Crea la estructura de la tarjeta
                const imageSrc = asset.Image ? `data:image/png;base64,${asset.Image}` : "/Content/imagenes/Imagen.jpeg";
                card.innerHTML = `
            <div class="card" style="min-height: 450px !important;" >
                <div id="conteinerImg_${asset.TipoAssetID}" class="imgcontenedor"> <img src="${imagenUrl}" id="imgcard" class="card-img-top" alt="..."> </div>
                <div class="card-body">
                    <h5 class="card-title">${asset.Title} </h5>
                    <p class="card-text">${asset.Description}</p>
                    <a href='/group/ShowAssets?id=${asset.ID}' target="_blank" class="btn btn-primary"><img src="/Content/imagenes/See-All-content-2.png" alt="Icono de actividades" style="width: 30px; height: auto;"> Ver Contenido
                    </a>

                </div>
            </div>
        `;

                cardContainer.appendChild(card);
            });
        }
    }
    else
    {
        if (vertodos == true) {
            data.Data.forEach(asset => {
                let imagenUrl;
                if (asset.TipoAssetID === 1) {
                    imagenUrl = "/Content/imagenes/TeoríayEjercicios.png";
                }
                else if (asset.TipoAssetID === 2) {
                    imagenUrl = "/Content/imagenes/Solucionesdeloejercicios.png";

                }
                else if (asset.TipoAssetID === 3) {
                    imagenUrl = "/Content/imagenes/Actividades.png";

                }
                else if (asset.TipoAssetID === 4) {
                    imagenUrl = "/Content/imagenes/RespuestasdelasActividades.png";

                }
                else if (asset.TipoAssetID === 5) {
                    imagenUrl = "/Content/imagenes/Resumen.png";

                }
                else if (asset.TipoAssetID === 6) {
                    imagenUrl = "/Content/imagenes/EjerciciosAdicionales.png";

                }
                else if (asset.TipoAssetID === 7) {
                    imagenUrl = "/Content/imagenes/Autoevaluación.png";

                }
                else if (asset.TipoAssetID === 9) {
                    imagenUrl = "/Content/imagenes/Profundización";

                }

                //imagenUrl = "/Content/imagenes/Theory-and-Exercises.png";

                const card = document.createElement("div");
                card.className = "col mb-4";
                card.id = "cardContenedor_";
                card.setAttribute("data-value", `${asset.TipoAssetID}`);

                // Crea la estructura de la tarjeta
                const imageSrc = asset.Image ? `data:image/png;base64,${asset.Image}` : "/Content/imagenes/Imagen.jpeg";
                card.innerHTML = `
                <div class="card" style="min-height: 450px !important;">
                    <div id="conteinerImg_${asset.TipoAssetID}" class="imgcontenedor"> <img src="${imagenUrl}" id="imgcard" class="card-img-top" alt="..."> </div>
                    <div class="card-body">
                        <h5 class="card-title">${asset.Title} </h5>
                        <p class="card-text">${asset.Description}</p>
                        <a href='/group/ShowAssets?id=${asset.ID}' target="_blank" class="btn btn-primary"><img src="/Content/imagenes/See-All-content-2.png" alt="Icono de actividades" style="width: 30px; height: auto;"> Ver Contenido
                        </a>

                    </div>
                </div>
            `;

                cardContainer.appendChild(card);
            });
        }
        
    }
}


document.addEventListener("DOMContentLoaded", function () {
    const filtros = document.querySelectorAll(".filtro");
    const imagenes = document.querySelectorAll(".tarjetaImagen");
 
    imagenes.forEach(imagen => {
        imagen.addEventListener("click", function () {
            // Obtener el data-value de la imagen para identificar el checkbox correcto
            $('#collapseExample').collapse('hide');
            const dataValue = this.getAttribute('data-value');
            const checkbox = document.querySelector(`#f${dataValue}`);

            // Cambiar el estado del checkbox
            checkbox.checked = !checkbox.checked;

            // Llamar a la función de actualizar filtros activos después de cambiar el estado del checkbox
            actualizarFiltrosActivos();
        });
    });

    function actualizarFiltrosActivos() {
        const valoresActivos = [];

        // Recopilar los valores de los checkboxes que están marcados
        filtros.forEach(filtro => {
            if (filtro.checked) {
                valoresActivos.push(filtro.value);
            }
        });

        // Llamar a la función que maneja la respuesta de los filtros activos
        obtenerDatosYAgregarElemento(valoresActivos);
    }

});
              

function obtenerDatosgetGruposYAgregarElemento() {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Group/id?id=${idGrupo}`;
    var nameGrupo = document.getElementById('Grupo');



    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {

            nameGrupo.textContent = `${data.Data.Description}`;

        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

obtenerDatosYAgregarElemento();
obtenerDatosSubtematicasYAgregarElemento();
obtenerDatosgetGruposYAgregarElemento();

function obtenerdatosnav() {
    var ul = document.getElementById('accordionExample');
    var spanSubtematicasHeader = document.getElementById('subtematicasHeader');
    var spanUnidadHeader = document.getElementById('unidadHeader');
    var profesor = document.getElementById('profesor');
    var tematicaNameElements = document.querySelectorAll('#tematicatxt');
    var SubtematicaHijosElements = document.querySelectorAll('#SubtematicaHijos');

    function handleTematicaClick(element) {
        var tematicaValue = element.getAttribute('value');
        spanSubtematicasHeader.textContent = "";
        spanUnidadHeader.textContent = tematicaValue;
    }

    function handleSubtematicaClick(element) {
        var subtematicaValue = element.getAttribute('value');
        console.log(subtematicaValue);
        spanSubtematicasHeader.textContent = subtematicaValue;
    }

    function handleAccordionItemClick(element) {
        var subtematicaId = element.getAttribute('data-tematica-id-item');
        console.log(subtematicaId);

        //profesor.textContent = 'Profesor Prueba';
        document.getElementById('idfantastaSubtematica').value = subtematicaId;

        obtenerDatosGetByTematicYAgregarElemento(subtematicaId);
    }

    ul.querySelectorAll('[data-tematica-id-item]').forEach((element) => {
        element.addEventListener('click', function () {
            handleAccordionItemClick(element);
        });
    });

    tematicaNameElements.forEach(function (element) {
        element.addEventListener('click', function () {
            handleTematicaClick(element);
        });
    });

    SubtematicaHijosElements.forEach(function (element) {
        element.addEventListener('click', function () {
            handleSubtematicaClick(element);
        });
    });
}

