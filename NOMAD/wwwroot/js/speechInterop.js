window.nomadSpeech = {
    speak: function (text, langCode) {
        if ('speechSynthesis' in window) {
            window.speechSynthesis.cancel();

            let utterance = new SpeechSynthesisUtterance(text);
            utterance.lang = langCode || 'tr-TR'; 
            utterance.rate = 0.9;
            utterance.pitch = 1.1;

            window.speechSynthesis.speak(utterance);
        } else {
            console.warn("Text-to-Speech not supported in this browser.");
        }
    }
};
