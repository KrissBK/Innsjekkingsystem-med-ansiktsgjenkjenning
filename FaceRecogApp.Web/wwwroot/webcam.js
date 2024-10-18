
// Funksjonaliteten i denne filen er basert på kode fra: https://wellsb.com/csharp/aspnet/blazor-webcam-capture
function startVideo(src) {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            let video = document.getElementById(src);

            if ("srcObject" in video) {
                video.srcObject = stream;
            } else {
                video.src = window.URL.createObjectURL(stream);
            }
            video.onloadedmetadata = function (e) {
                video.play();
            };
            //mirror image
            video.style.webkitTransform = "scaleX(-1)";
            video.style.transform = "scaleX(-1)";
        });
    }
}

function getFrameCheckIn(src, dest, dotNetHelper) {
    let video = document.getElementById(src);
    let canvas = document.getElementById(dest);
    canvas.getContext('2d').drawImage(video, 0, 0, 320, 240);

    let dataUrl = canvas.toDataURL("image/jpeg");

    dotNetHelper.invokeMethodAsync('ProcessImageCheckIn', dataUrl);
    stopVideo(src);
}

function getFrameAddFace(src, dest, dotNetHelper) {
    let video = document.getElementById(src);
    let canvas = document.getElementById(dest);
    canvas.getContext('2d').drawImage(video, 0, 0, 320, 240);

    let dataUrl = canvas.toDataURL("image/jpeg");

    dotNetHelper.invokeMethodAsync('ProcessImageAddFace', dataUrl);
    stopVideo(src);
}

function stopVideo(src) {
    let video = document.getElementById(src);
    if (video && "srcObject" in video) {
        const stream = video.srcObject;
        if (stream) {
            const tracks = stream.getTracks();
            tracks.forEach(track => track.stop());
        }
        video.srcObject = null;
    }
}