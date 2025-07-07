document.addEventListener('DOMContentLoaded', function () {
    const botones = document.querySelectorAll('.sidebar-link');
    const contenido_botones = document.querySelectorAll('.pagina-contenido');
    const boton_enviar = document.getElementById("btn-envio");
    const txt = document.getElementById("txtArea");
    const darkModeToggle = document.getElementById('dark-mode-toggle');

    // ---- INICIO: Lógica del Modo Oscuro ----
    const setDarkMode = (isDark) => {
        if (isDark) {
            document.body.classList.add('dark-mode');
            if(darkModeToggle) darkModeToggle.checked = true;
            localStorage.setItem('darkMode', 'enabled');
        } else {
            document.body.classList.remove('dark-mode');
            if(darkModeToggle) darkModeToggle.checked = false;
            localStorage.setItem('darkMode', 'disabled');
        }
    };

    // Revisa la preferencia guardada al cargar la página
    if (localStorage.getItem('darkMode') === 'enabled') {
        setDarkMode(true);
    }

    // Listener para el interruptor
    if (darkModeToggle) {
        darkModeToggle.addEventListener('change', () => {
            setDarkMode(darkModeToggle.checked);
        });
    }
    // ---- FIN: Lógica del Modo Oscuro ----

    function ApagarSecciones() {
        contenido_botones.forEach(contenido => {
            contenido.style.display = 'none';
        });
    }

    function EncenderSeccion(idSeccion) {
        const seccion_especifico = document.getElementById(idSeccion);
        if (seccion_especifico) {
            seccion_especifico.style.display = 'block';
        }
    }

    function actualizarBoton(BotonPresionado) {
        botones.forEach(b => {
            b.classList.remove('active');
        });
        if (BotonPresionado) {
            BotonPresionado.classList.add('active');
        }
    }

    botones.forEach(boton => {
        boton.addEventListener('click', e => {
            e.preventDefault();
            const id = boton.dataset.target;
            ApagarSecciones();
            EncenderSeccion(id);
            actualizarBoton(boton);
        });
    });

    function Mensaje() {
        const t = txt.value.length;
        if (t >= 20) {
            swal('Enviado', 'El mensaje fue enviado con éxito', 'success');
            txt.value = "";
        } else {
            swal('Error', 'El mensaje no fue enviado (LONGITUD MÍNIMA 20 CARACTERES)', 'error');
        }
    }

    if (boton_enviar) {
        boton_enviar.addEventListener('click', Mensaje);
    }
});

 