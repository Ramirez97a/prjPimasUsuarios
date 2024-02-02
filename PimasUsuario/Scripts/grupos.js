

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
    }, 2000);



    $('#filtros').on('click', function (event) {
        event.preventDefault();
        var idTematica = document.getElementById('idfantastaSubtematica').value;

        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');
        obtenerDatosGetByTematicYAgregarElemento(idTematica);

    });
    $('#filtrosAll').on('click', function (event) {
        obtenerDatosYAgregarElemento();

    });

    $(document).on('click', '#vertodos', function () {
        event.preventDefault();
        var elements = document.querySelectorAll('.show');
        var tematicaElement = document.getElementById('tematicatxt');
        var lblhidden = document.querySelectorAll('[id*="lblhidden"]');

        obtenerDatosYAgregarElemento();

        elements.forEach(function (element) {
            element.classList.remove('show');
        });

        tematicaElement.setAttribute('aria-expanded', 'false');


        // Itera sobre los elementos y elimina el atributo style
        lblhidden.forEach(function (element) {
            element.style.display = "none";
        });

    })

    $(document).on('click', '#subtematica', function () {
        event.preventDefault();
    })
    $(document).on('click', '#VerGrupos', function () {

        window.location.href = `/group/Index?`;

    })

    $(document).on('click', '#filtros', function () {
        $('#collapseExample').collapse('hide');

        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');
    })

    $(document).on('click', '#tematicatxt', function () {
        var elements = document.querySelectorAll('[id*="lblhidden"]');

        // Itera sobre los elementos y elimina el atributo style
        elements.forEach(function (element) {
            element.removeAttribute("style");
        });
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
            agregarAssest(data);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}


function obtenerDatosYAgregarElemento() {
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

            agregarAssest(data);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}



function obtenerDatosSubtematicasYAgregarElemento() {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Group/tematics?id=${idGrupo}`;

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
            console.log(data);
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
            <a href="#" class="tematicaid"  id="VerGrupos">
                <div>
                    <span>Grupos</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="tematicaid"  id="vertodos">
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

        nuevoLi.innerHTML = `
                    <input id="group-${tematicaId}" type="checkbox" hidden />
                    <label for="group-${tematicaId}"  class="tematicaid" id="tematicatxt"  data-tematica-id-item="${tematicaId}" value="${tematicasArray[0]?.Nombre}">
                        <span  class="fa fa-angle-right"></span>${tematicasArray[0]?.Nombre || ''}
                    </label>

                    <ul class="group-list">
                        ${tematicasArray[0]?.Hijos.map(subtema => {
            const tieneNietos = tematicasArray[0]?.Nietos.some(nieto => nieto.ParentTematicaID === subtema.TematicaID);

            return tieneNietos ? `
                                <li>
                                    <input id="sub-group-${subtema.TematicaID}" type="checkbox" hidden />
                                    <label for="sub-group-${subtema.TematicaID}" data-tematica-id-item="${subtema.TematicaID}" value="${subtema.NombreTematica}" id="SubtematicaHijos">
                                        <span  class="fa fa-angle-right"></span>${subtema.NombreTematica}
                                    </label>
                                    <ul class="sub-group-list">
                                        ${tematicasArray[0]?.Nietos.filter(nieto => nieto.ParentTematicaID === subtema.TematicaID).map(nieto => `
                                            <li>
                                                <a href="#" data-tematica-id-item="${nieto.TematicaID}">${nieto.NombreTematica}</a>
                                            </li>`).join('')}
                                    </ul>
                                </li>
                            ` : `
                                <li>
                                    <label for="sub-group-${subtema.TematicaID}" data-tematica-id-item="${subtema.TematicaID}" value="${subtema.NombreTematica}" id="SubtematicaHijos">
                                        <span ></span>${subtema.NombreTematica}
                                    </label>

                                </li>`;
        }).join('')}
                    </ul>
         `;


        ul.appendChild(nuevoLi);
    }

    obtenerdatosnav();
}



function agregarAssest(data) {
    const cardContainer = document.getElementById("cardContairnerAssest");
    cardContainer.innerHTML = "";
    data.Data.forEach(asset => {
        const card = document.createElement("div");
        card.className = "col mb-2";
        card.style.maxWidth = "300px";

        // Crea la estructura de la tarjeta
        const imageSrc = asset.Image ? `data:image/png;base64,${asset.Image}` : "/Content/imagenes/Imagen.jpeg";
        card.innerHTML = `
            <div class="card h-100">
                <img src="${imageSrc}" class="card-img-top" alt="Asset Image" style="width: 100%; height: 100%;">
                <div class="card-body d-flex flex-column align-items-center justify-content-center">
                    <h6 style="color: #038512; font-weight:bold;" class="text-center">${asset.Title}</h6>
                    <p class="text-center">${asset.Description}</p>
                    <a href='/group/ShowAssets?id=${asset.ID} 'target="_blank" class="btn btn-primary">Ver contenido</a>
                </div>
            </div>
        `;
        cardContainer.appendChild(card);
    });
}

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

        profesor.textContent = 'Profesor Prueba';
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


