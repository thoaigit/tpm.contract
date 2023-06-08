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
}

// Mở tab đầu tiên mặc định
document.getElementsByClassName("tablinks")[0].click();



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

document.getElementById("openPopup").addEventListener("click", function () {
    document.getElementById("myPopup").style.display = "block";
    document.getElementById("overlay").style.display = "block";
    document.body.classList.add("popup-active");
});

document.getElementById("closePopup").addEventListener("click", function () {
    document.getElementById("myPopup").style.display = "none";
    document.getElementById("overlay").style.display = "none";
    document.body.classList.remove("popup-active");

    // Xoá các dữ liệu đã nhập
    document.getElementById("unitPrice").value = "";
    document.getElementById("quantity").value = "";
    document.getElementById("totalAmount").value = "";
});

// Tính toán tự động khi nhập đơn giá và số lượng
document.getElementById("unitPrice").addEventListener("input", calculateTotalAmount);
document.getElementById("quantity").addEventListener("input", calculateTotalAmount);

function calculateTotalAmount() {
    var unitPrice = document.getElementById("unitPrice").value;
    var quantity = document.getElementById("quantity").value;
    var totalAmount = parseFloat(unitPrice) * parseFloat(quantity);

    document.getElementById("totalAmount").value = totalAmount.toFixed(2);
}

