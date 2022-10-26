document.getElementById('chkFlat').addEventListener('change', toggleBlockFlat);
document.getElementById('chkHouse').addEventListener('change', toggleBlockHouse);
document.getElementById('chkRoom').addEventListener('change', toggleBlockRoom);

function toggleBlockFlat(evt) {
    document.querySelector('.blockFlat').classList.toggle('hidden-div');
}
function toggleBlockHouse(evt) {
    document.querySelector('.blockHouse').classList.toggle('hidden-div');
}
function toggleBlockRoom(evt) {
    document.querySelector('.blockRoom').classList.toggle('hidden-div');
}