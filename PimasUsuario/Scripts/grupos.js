

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
    //$('#vertodos').on('click', function (event) {
    
      

    //});

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
    $(document).on('click', '#btnhome', function () {
        window.location.href = `/group/Index`;

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

    $(document).on('click', '#tematicatxt', function () {
        
        var tematicaid;
        var elementosA = document.querySelectorAll('[data-bs-toggle="collapse"]');
        var spanUnidad = document.getElementById('unidadHeader');
        elementosA.forEach(function (elementoA) {
            if (elementoA.getAttribute('aria-expanded') === 'true') {
                tematicaid = elementoA.getAttribute('value');

                if (tematicaid == 1) {
                    spanUnidad.textContent = 'Limites';
                }
                else {
                    spanUnidad.textContent = 'Derivadas';
                }
            }
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
          
            agregarElementosTematicas(data);
           /* agregarAssest(data);*/
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

function agregarElementosTematicas(data) {
    var ul = document.getElementById('accordionExample');
    const tematicas = [
        { id: 1, nombre: 'Limites' },
        { id: 2, nombre: 'Derivadas' }
    ];

    // Limpiar el contenido actual de la lista
    ul.innerHTML = '';

    // Agregar el contenido HTML antes del bucle forEach
    var contenidoInicial = `
        <li class="menu active ">
            <a href="#" class="dropdown-toggle" id="btnhome">
                <div>
                    <img src="/Content/imagenes/Logo_inicio.png" alt="logo" width="30px" height="30px" style="margin-right:40px;"><span>PIMAS</span>
                </div>
            </a>
           
        </li>
        <li class="menu  ">
            <a href="#" class="dropdown-toggle" id="vertodos" >
                <div>
                   <span>Grupos</span>
                </div>
            </a>

        </li>
        <li class="menu  ">
            <a href="#" class="dropdown-toggle" id="vertodos" >
                <div>
                   <span>Todos los Archivos</span>
                </div>
            </a>

        </li>


    `;
    ul.innerHTML += contenidoInicial;

    // Objeto para almacenar temáticas agrupadas por ParentTematicaID
    var tematicasAgrupadas = {};

    data.Data.forEach((item, index) => {
        if (item.NombreTematica && item.ParentTematicaID) {
            const tematicaId = item.ParentTematicaID;
            const tematica = item.NombreTematica;
            const tematicaIDItem = item.TematicaID; // Nuevo - obtener el TematicaID
            // Crear un array si no existe para el ParentTematicaID actual
            tematicasAgrupadas[tematicaId] = tematicasAgrupadas[tematicaId] || [];

            // Agregar la tematica al array correspondiente
            tematicasAgrupadas[tematicaId].push({ tematica, tematicaIDItem });
        }
    });

    // Recorrer el objeto y agregar las temáticas al DOM
    for (const [tematicaId, tematicasArray] of Object.entries(tematicasAgrupadas)) {
        var nuevoLi = document.createElement('li');
        nuevoLi.className = 'menu';

        const tematicaEncontrada = tematicas.find(t => t.id == tematicaId);

        nuevoLi.innerHTML = `
            <a href="#tematica_${tematicaId}" data-bs-toggle="collapse" aria-expanded="false" class="dropdown-toggle" id="tematicatxt" value="${tematicaId}" >
                <div>
                    <span id="idUnidad">Unidad: ${tematicaEncontrada.nombre}</span>
                </div>
                <div>
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right"><polyline points="9 18 15 12 9 6"></polyline></svg>
                </div>
            </a>
            <ul class="collapse submenu list-unstyled" id="tematica_${tematicaId}" data-bs-parent="#accordionExample">
                ${tematicasArray.map(({ tematica, tematicaIDItem }) => `<li><a href="component_tabs.html"  data-tematica-id-item="${tematicaIDItem}"  id="subtematica">${tematica}</a></li>`).join('')}
            </ul>
        `;
        ul.appendChild(nuevoLi);
    }

    // Agregar evento para gestionar el estado de los elementos
    ul.querySelectorAll('[data-bs-toggle="collapse"]').forEach((element) => {
        element.addEventListener('click', function() {
            const collapseTarget = document.querySelector(element.getAttribute('href'));
            ul.querySelectorAll('.collapse.show').forEach((collapse) => {
                if (collapse !== collapseTarget) {
                    collapse.classList.remove('show');
                }
            });
        });
    });
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
//obtenerDatosgetGruposYAgregarElemento();



function obtenerdatosnav() {
    var tematicaid;    
    var subtematica;
    var subtematicaTxt;
    var ul = document.getElementById('accordionExample');    
    var elementosA = document.querySelectorAll('[data-bs-toggle="collapse"]');
    var spanUnidad = document.getElementById('subtematicasHeader');
    var profesor = document.getElementById('profesor');
    ul.querySelectorAll('[data-tematica-id-item]').forEach((element) => {

        element.addEventListener('click', function () {
            subtematica = element.getAttribute('data-tematica-id-item');
            subtematicaTxt = element.innerHTML;


            elementosA.forEach(function (elementoA) {
                if (elementoA.getAttribute('aria-expanded') === 'true') {
                    tematicaid = elementoA.getAttribute('value');


                }
            });

            spanUnidad.textContent = `${subtematicaTxt}`;
            profesor.textContent = 'Profesor Prueba';
            document.getElementById('idfantastaSubtematica').value = `${subtematica}`;

            obtenerDatosGetByTematicYAgregarElemento(subtematica);

           
          
           
        });
        
    });
}

  