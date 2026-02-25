mergeInto(LibraryManager.library, {
  ResumeAudioContext: function () {
    if (typeof unityAudioContext !== "undefined") {
      if (unityAudioContext.state !== "running") {
        unityAudioContext.resume();
      }
    }
  }
});