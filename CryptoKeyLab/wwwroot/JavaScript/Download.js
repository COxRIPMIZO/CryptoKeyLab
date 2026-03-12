//window.DownloadFile = (filename, contentType, content) => {
//    // Create a blob with the content provided
//    const blob = new Blob([content], { type: contentType });
//    // Create a link element
//    const link = document.createElement('a');
//    // Set the download attribute with the filename
//    link.download = filename;
//    // Create a URL for the blob and set it as the href attribute
//    link.href = window.URL.createObjectURL(blob);
//    // Append the link to the body (required for Firefox)
//    document.body.appendChild(link);
//    // Programmatically click the link to trigger the download
//    link.click();
//    // Clean up by removing the link and revoking the object URL
//    document.body.removeChild(link);
//    window.URL.revokeObjectURL(link.href);
//};

window.DownloadFile = (fileName, contentType, base64Data) => {
    // Convert base64 back into raw bytes
    const bytes = Uint8Array.from(atob(base64Data), c => c.charCodeAt(0));
    const blob = new Blob([bytes], { type: contentType });

    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
    URL.revokeObjectURL(link.href);
};