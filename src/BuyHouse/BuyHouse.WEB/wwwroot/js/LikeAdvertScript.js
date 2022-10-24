function LikeFlatAdvert() {
    $(document).ready(function () {
        $(".btn-like-flatadvert").on("click", function () {
            var advertid = $(this).attr("data-flatAdvertId");
            var div_ = document.querySelector("#like_advert");
            var url = 'FlatAdvert/LikeFlatAdvert/?flatAdvertId=' + advertid;
            $.ajax({
                type: 'GET',
                url: url,
                success: function (result) {
                        div_.innerHTML = null;
                        div_.innerHTML += result;
                },
                error: function (req, status) {
                    console.log(status);
                }
            });
        });
    })
}

function LikeHouseAdvert() {
    $(document).ready(function () {
        $(".btn-like-houseadvert").on("click", function () {
            var advertid = $(this).attr("data-houseAdvertId");
            var div_ = document.querySelector("#like_advert");
            var url = 'HouseAdvert/LikeHouseAdvert/?houseAdvertId=' + advertid;
            $.ajax({
                type: 'GET',
                url: url,
                success: function (result) {
                    div_.innerHTML = null;
                    div_.innerHTML += result;
                },
                error: function (req, status) {
                    console.log(status);
                }
            });
        });
    })
}

function LikeRoomAdvert() {
    $(document).ready(function () {
        $(".btn-like-roomadvert").on("click", function () {
            var advertid = $(this).attr("data-roomAdvertId");
            var div_ = document.querySelector("#like_advert");
            var url = 'RoomAdvert/LikeRoomAdvert/?roomAdvertId=' + advertid;
            $.ajax({
                type: 'GET',
                url: url,
                success: function (result) {
                    div_.innerHTML = null;
                    div_.innerHTML += result;
                },
                error: function (req, status) {
                    console.log(status);
                }
            });
        });
    })
}

LikeFlatAdvert();
LikeHouseAdvert();
LikeRoomAdvert();