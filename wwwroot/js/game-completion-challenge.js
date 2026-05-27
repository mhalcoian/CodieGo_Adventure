function showChallengeCompletionOverlay(options) {

    document.getElementById("completionTitleChallenge").innerText =
        options.title ?? "COMPLETED";

    document.getElementById("completionMessageChallenge").innerText =
        options.message ?? "";

    const minutes = Math.floor(options.timeLeft / 60);
    const seconds = options.timeLeft % 60;

    const formattedTime =
        `${String(minutes).padStart(2, "0")}:${String(seconds).padStart(2, "0")}`;

    document.getElementById("completionScore").innerText = options.score;
    document.getElementById("completionTime").innerText = formattedTime;
    document.getElementById("completionTotal").innerText = options.totalScore;

    if (options.exitUrl) {
        document.getElementById("btnExitChallenge").href = options.exitUrl;
    }

    document.getElementById("completionOverlayChallenge").style.display = "flex";
}