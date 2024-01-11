$(document).ready(function () {
    alert("jo");
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
    }, 19000);

   /* obtenerDatosYAgregarElemento();*/

    $('#filtros').on('click', function (event) {
        event.preventDefault();
        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');

    });


    $(document).on('click', '#filtros', function () {
        $('#collapseExample').collapse('hide');

        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');
    })

    const urlParams = new URLSearchParams(window.location.search);

    // Obtener el valor del parámetro 'id'
    const id = urlParams.get('id');
    console.log(id);
   
  
});

function obtenerDatosYAgregarElemento() {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `https://localhost:44366/api/Assets/getByGroup?id=${idGrupo}`;

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
            agregarAssest(data);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

function agregarElementosTematicas(data) {
    var ul = document.getElementById('accordionExample');
   

    // Limpiar el contenido actual de la lista
    ul.innerHTML = '';

    data.Data.forEach((item, index) => {
        if (item.Tematicas && item.Tematicas.NombreTematica) {
            const tematicaId = item.Tematicas.TematicaID;
            const tematica = item.Tematicas.NombreTematica;

            var nuevoLi = document.createElement('li');
            nuevoLi.className = 'menu';

            nuevoLi.innerHTML = `
                  <li class="menu active ">
                        <a href="#" class="dropdown-toggle" id="btnhome">
                            <div class="">
                                <img src="/Content/imagenes/Logo_inicio.png" alt="logo" width="30px" height="30px" style="margin-right:40px;"<span>PIMAS</span>
                            </div>

                        </a>

                    </li>
                <a href="#tematica" data-bs-toggle="collapse" aria-expanded="false" class="dropdown-toggle" value="${tematicaId}">
                    <div>
                        <span>Unidad: ${tematica}</span>
                    </div>
                    <div>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right"><polyline points="9 18 15 12 9 6"></polyline></svg>
                    </div>
                </a>
                <ul class="collapse submenu list-unstyled" id="tematica" data-bs-parent="#accordionExample">
                    <li><a href="component_tabs.html">reactivar</a></li>
                    <li><a href="component_accordion.html">reactivar</a></li>
                    <li><a href="component_accordion.html">reactivar</a></li>
                    <li><a href="component_accordion.html">reactivar</a></li>
                </ul>
            `;
            ul.appendChild(nuevoLi);
        }
    });
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


obtenerDatosYAgregarElemento();



