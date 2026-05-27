function openEditModal() {
    document.getElementById("editProfileModal").style.display = "flex";
}

function closeEditModal() {
    document.getElementById("editProfileModal").style.display = "none";
}

function previewModalImage(event) {
    const reader = new FileReader();
    reader.onload = () =>
        document.getElementById("modalPreview").src = reader.result;

    reader.readAsDataURL(event.target.files[0]);
}

async function saveProfileChanges() {

    const form = document.getElementById("editProfileForm");
    const formData = new FormData(form);

    await fetch('/Users/UpdateProfileDetail', {
        method: 'POST',
        body: formData
    });

    location.reload(); // refresh profile data
}