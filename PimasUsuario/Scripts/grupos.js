


document.addEventListener('DOMContentLoaded', function () {
    // Función que se ejecutará cuando la página esté cargada
    function agregarElemento() {
        // Obtén la referencia a la etiqueta ul existente
        var ul = document.getElementById('accordionExample');

        // Crea un nuevo elemento li
        var nuevoLi = document.createElement('li');
        nuevoLi.className = 'menu';  // Agrega la clase 'menu' al nuevo elemento li

        // Contenido del nuevo li
        nuevoLi.innerHTML = `
                <a href="#apps" data-bs-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                    <div>
                        <span>Unidad: Limites</span>
                    </div>
                    <div>
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right"><polyline points="9 18 15 12 9 6"></polyline></svg>
                    </div>
                </a>
                <ul class="collapse submenu list-unstyled" id="apps" data-bs-parent="#accordionExample">
                    <li><a href="component_tabs.html">reactivar</a></li>
                    <li><a href="component_accordion.html">reactivar</a></li>
                    <li><a href="component_accordion.html">reactivar</a></li>
                    <li><a href="component_accordion.html">reactivar</a></li>
                </ul>
            `;

        // Agrega el nuevo li a la lista ul existente
        ul.appendChild(nuevoLi);
    }

    agregarElemento();
});