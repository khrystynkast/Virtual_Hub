document.addEventListener("DOMContentLoaded", function () {
    const sections = document.querySelectorAll("section");
    const buttons = document.querySelectorAll(".nav-icons button");

    // Функція для визначення активної секції
    function updateActiveButton() {
        let currentSection = null;

        sections.forEach((section) => {
            const rect = section.getBoundingClientRect();
            if (rect.top <= window.innerHeight / 2 && rect.bottom >= window.innerHeight / 2) {
                currentSection = section;
            }
        });

        buttons.forEach((button) => {
            button.classList.remove("active");
            if (currentSection && button.dataset.scroll === currentSection.id) {
                button.classList.add("active");
            }
        });
    }

    // Плавний скрол до секції
    buttons.forEach(button => {
        button.addEventListener("click", function () {
            const targetId = this.getAttribute("data-scroll");
            const targetElement = document.getElementById(targetId);
            if (targetElement) {
                window.scrollTo({
                    top: targetElement.offsetTop - 50,
                    behavior: "smooth"
                });
            }
        });
    });

    window.addEventListener("scroll", updateActiveButton);
    updateActiveButton(); // Виклик при завантаженні сторінки
});

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('[data-scroll]').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            const targetId = this.getAttribute('data-scroll');
            const targetElement = document.getElementById(targetId);
            if (targetElement) {
                targetElement.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
});



