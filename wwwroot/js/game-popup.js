function showGamePopup(type, title, message) {
    const overlay = document.getElementById("gamePopupOverlay");
    const icon = document.getElementById("popupIcon");
    const popupTitle = document.getElementById("popupTitle");
    const popupMessage = document.getElementById("popupMessage");

    popupTitle.textContent = title;
    popupMessage.textContent = message;

    icon.className = "popup-icon";

    switch (type) {
        case "success":
            icon.textContent = "✔";
            icon.classList.add("popup-success");
            break;
        case "error":
            icon.textContent = "✖";
            icon.classList.add("popup-error");
            break;
        case "warning":
            icon.textContent = "⚠";
            icon.classList.add("popup-warning");
            break;
    }

    overlay.style.display = "flex";
}

function closeGamePopup() {
    document.getElementById("gamePopupOverlay").style.display = "none";
}