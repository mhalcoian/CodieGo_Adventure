let confirmCallback = null;

function showConfirmPopup(title, message, onConfirm) {
    const overlay = document.getElementById("confirmPopupOverlay");

    document.getElementById("confirmTitle").textContent = title;
    document.getElementById("confirmMessage").textContent = message;

    confirmCallback = onConfirm;

    overlay.style.display = "flex";
}

function closeConfirmPopup() {
    document.getElementById("confirmPopupOverlay").style.display = "none";
}

document.getElementById("confirmYesBtn").onclick = () => {
    closeConfirmPopup();

    if (confirmCallback) {
        confirmCallback();
    }
};

document.getElementById("confirmNoBtn").onclick = () => {
    closeConfirmPopup();
};