function showAssessmentCompletionOverlay(options) {

    document.getElementById("completionTitleAssessment").innerText =
        options.title ?? "COMPLETED";

    document.getElementById("completionScoreAssessment").innerText =
        options.scoreAssessment ?? "0";

    document.getElementById("completionMessageAssessment").innerText =
        options.message ?? "";

    document.getElementById("completionDescriptionAssessment").innerText =
        options.description ?? "";

    if (options.exitUrl) {
        document.getElementById("btnExitAssessment").href =
            options.exitUrl;
    }

    if (options.retryUrl) {
        document.getElementById("btnRetryAssessment").href =
            options.retryUrl;
    }

    if (options.proceedUrl) {
        document.getElementById("btnProceedAssessment").href =
            options.proceedUrl;
    }

    document.getElementById("completionOverlayAssessment").style.display = "flex";
}