function showGameToast(type, title, message, duration = 3000) {
    const container = document.getElementById("gameToastContainer");

    const toast = document.createElement("div");
    toast.className = `game-toast toast-${type}`;

    toast.innerHTML = `
        <div class="toast-title">${title}</div>
        <div class="toast-message">${message}</div>
    `;

    container.appendChild(toast);

    toast.offsetHeight;

    toast.classList.add("show");

    setTimeout(() => {
        toast.classList.remove("show");
        toast.classList.add("toast-exit");
    }, duration);

    setTimeout(() => {
        toast.remove();
    }, duration + 700);
}