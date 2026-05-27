function showCompletionOverlay(options) {

    document.getElementById("completionTitle").innerText =
        options.title ?? "COMPLETED";

    document.getElementById("completionMessage").innerText =
        options.message ?? "";

    document.getElementById("completionDescription").innerText =
        options.description ?? "";

    if (options.rewardImage) {
        document.getElementById("completionReward").src =
            options.rewardImage;
    }

    if (options.exitUrl) {
        document.getElementById("btnExit").href =
            options.exitUrl;
    }

    if (options.retryUrl) {
        document.getElementById("btnRetry").href =
            options.retryUrl;
    }

    if (options.proceedUrl) {
        document.getElementById("btnProceed").href =
            options.proceedUrl;
    }

    document.getElementById("completionOverlay").style.display = "flex";
}