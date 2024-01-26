const navshow = () => {
    const burger = document.querySelector('.burger');
    const nav = document.querySelector('.navbar-links');
    const navlinks = document.querySelectorAll('.navbar-links li');
    const navbar = document.querySelector('.header');
    const logo = document.querySelector('.navbar-logo img');


    if (window.innerWidth < 1000) {
        logo.src = "/../Media/StaticImage/Siyah.png";
    }
    else {
        logo.src = "/../Media/StaticImage/logo.png"
    }

    window.addEventListener("scroll", () => {

      
        if (window.scrollY > 100) {
            navbar.classList.add("move")
            logo.src ="/../Media/StaticImage/Siyah.png"
        }
        else {
            navbar.classList.remove("move")
            if (window.innerWidth < 1000) {
                logo.src = "/../Media/StaticImage/Siyah.png";
            }
            else {
                logo.src = "/../Media/StaticImage/logo.png"
            }
            
        }    
    })


    burger.addEventListener('click', () => {

        nav.classList.toggle('burger-active');
        navlinks.forEach((link, index) => {
            if (link.style.animation) {
                link.style.animation = ``
            }
            else {
                link.style.animation = `linksfade 0.5s ease forwards ${index / 4 + 0.2}s `
            }
        });

        burger.classList.toggle('burger-close');
    });
}

navshow();