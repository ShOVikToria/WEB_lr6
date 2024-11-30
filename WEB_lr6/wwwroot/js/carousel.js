let currentIndex = 0;
const items = document.querySelectorAll('.carousel-item');
const items_m = document.querySelectorAll('.carousel-item-m');
const totalItems = items.length;

const isMobile = () => window.matchMedia("(max-width: 767px)").matches;

function showItem(index) {
    currentIndex = (index + totalItems) % totalItems;

    hideAll();

    if (isMobile()) {
        items_m[currentIndex].style.display = 'block';
    } else {
        items[currentIndex].style.display = 'block';
    }
}

function hideAll() {
    for (let i = 0; i < items.length; i++) {
        items[i].style.display = 'none';
    }
    for (let i = 0; i < items_m.length; i++) {
        items_m[i].style.display = 'none';
    }
}

function setupEventListeners() {
    document.getElementById('prevBtn').onclick = () => showItem(currentIndex - 1);
    document.getElementById('nextBtn').onclick = () => showItem(currentIndex + 1);
    document.getElementById('prevBtnm').onclick = () => showItem(currentIndex - 1);
    document.getElementById('nextBtnm').onclick = () => showItem(currentIndex + 1);
}

function setupScreenSizeListener() {
    const mediaQuery = window.matchMedia("(max-width: 767px)");
    mediaQuery.addEventListener("change", () => showItem(currentIndex));
}

function initCarousel() {
    setupEventListeners();
    setupScreenSizeListener();
    showItem(currentIndex);
}

initCarousel();
