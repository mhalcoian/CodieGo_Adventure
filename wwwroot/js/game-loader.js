function showGameLoader(title, message) {
    document.querySelector('.loader-title').innerText = title ?? 'Loading...';
    document.querySelector('.loader-message').innerText = message ?? 'Please wait';
    document.getElementById('gameLoaderOverlay').style.display = 'flex';
}

function hideGameLoader() {
    document.getElementById('gameLoaderOverlay').style.display = 'none';
}