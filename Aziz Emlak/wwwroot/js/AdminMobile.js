const Burger = () => {
    const burger = document.querySelector('.burger');
    const nav = document.querySelector('.satir');
    const navlinks = document.querySelectorAll('.panela');
   

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

Burger();
