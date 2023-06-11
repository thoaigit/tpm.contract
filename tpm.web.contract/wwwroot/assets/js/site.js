const toggleBtn = document.querySelector('.toggle-btn');
let isToggleOn = true;

toggleBtn.addEventListener('click', function () {
    isToggleOn = !isToggleOn;
    toggleBtn.innerHTML = isToggleOn ? '<i class="fa-solid fa-toggle-on fa-lg"></i>' : '<i class="fa-solid fa-toggle-off fa-lg"></i>';
});



function openTab(evt, tabName) {
    var i, tabcontent, tablinks;

    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].classList.remove("active");
    }

    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.classList.add("active");

    // Lưu tab hiện tại vào sessionStorage
    var currentTab = '#' + evt.currentTarget.id;
    sessionStorage.setItem('defaultTab', currentTab);
}

// Xử lý sự kiện khi tải trang
window.onload = function () {
    // Kiểm tra xem tab mặc định đã được lưu trong sessionStorage chưa
    var defaultTab = sessionStorage.getItem('defaultTab');
    if (defaultTab) {
        // Nếu đã có tab mặc định, nhấp vào tab tương ứng để duy trì tab hiện tại
        document.querySelector(defaultTab).click();
    } else {
        // Nếu chưa có tab mặc định, nhấp vào tab đầu tiên để đặt tab mặc định
        document.getElementById("tab1").click();
    }
};





//PopupTab3
document.getElementById("openPopup3").addEventListener("click", function () {
    document.getElementById("myPopup3").style.display = "block";
    document.getElementById("overlay").style.display = "block";
    document.body.classList.add("popup-active");
});



document.getElementById("closePopup3").addEventListener("click", function () {
    document.getElementById("myPopup3").style.display = "none";
    document.getElementById("overlay").style.display = "none";
    document.body.classList.remove("popup-active");

    // Xoá các dữ liệu đã nhập
    document.getElementById("unitPrice").value = "";
    document.getElementById("quantity").value = "";
    document.getElementById("totalAmount").value = "";
});

document.getElementById("openPopup4").addEventListener("click", function () {
    document.getElementById("myPopup4").style.display = "block";
    document.getElementById("overlay").style.display = "block";
    document.body.classList.add("popup-active");
});

document.getElementById("closePopup4").addEventListener("click", function () {
    document.getElementById("myPopup4").style.display = "none";
    document.getElementById("overlay").style.display = "none";
    document.body.classList.remove("popup-active");

    // Xoá các dữ liệu đã nhập
    document.getElementById("unitPrice").value = "";
    document.getElementById("quantity").value = "";
    document.getElementById("totalAmount").value = "";
});


function initializeScript() {
    $('#unitPrice').inputmask({
        alias: 'numeric',
        groupSeparator: ',',
        autoGroup: true,
        digits: 0,
        prefix: '',
        rightAlign: false
    });

    $('#unitPrice, #quantity').on('input', calculateTotalAmount);

    function calculateTotalAmount() {
        var unitPrice = parseFloat($('#unitPrice').val().replace(/,/g, ''));
        var quantity = parseFloat($('#quantity').val());
        var totalAmount = unitPrice * quantity;

        var formattedTotalAmount = totalAmount.toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',');

        $('#totalAmount').val(formattedTotalAmount + ' VNĐ');
    }
}

$(document).ready(function () {
    initializeScript();
});





