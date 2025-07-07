const links = document.querySelectorAll('.nav-item');
const sections = document.querySelectorAll('.pagina-contenido');
links.forEach(link => {
    link.addEventListener('click', (e) => {
        const targetId = link.getAttribute('data-target');
        links.forEach(l => l.classList.remove('active'));
        link.classList.add('active');
        sections.forEach(section => {
            section.classList.remove('active');
        });
        const targetSection = document.getElementById(targetId);
        if (targetSection) {
            targetSection.classList.add('active');
        }
    });
});