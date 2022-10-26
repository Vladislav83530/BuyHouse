function LikeAdvert(url_) {
    $(document).ready(function () {
        Array.from(document.getElementsByClassName('btn-like-advert')).forEach(element => {
            element.addEventListener('click', () => {           
                var advertid = element.dataset.flatadvertid;
                var div_ = element.nextElementSibling;
            var img = element.querySelector("#likeimg");
            var url = url_ + advertid;
            $.ajax({
                type: 'GET',
                url: url,
                success: function (result) {
                    var num = parseInt(div_.textContent.toString());
                    div_.innerHTML = null;
                    if (num < result) {
                        img.src = "https://cdn-icons-png.flaticon.com/512/2589/2589175.png"
                        div_.innerHTML += result;
                    }
                    else if (num > result) {
                        img.src = "https://cdn-icons-png.flaticon.com/512/2589/2589197.png"
                        div_.innerHTML += result;
                    }
                },
                error: function (req, status) {
                    console.log(status);
                }
            });
            })  
        });
    })
}