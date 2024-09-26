function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(() => {
        alert('Text copied to clipboard!');
    });
}
