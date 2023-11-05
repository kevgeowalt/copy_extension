
var _parent = document.getElementById('parent');

for (let index = 1; index <= 10; index++) {
    CreateChild(`Index ${index}`);
}
addEventToListItems();

function CreateChild(str) {
    var htmlStr = `<li>
            <div class="list-item">
                <a href="#">
                    ${str}
                    <i class="fa-regular fa-copy copy-icon"></i>
                </a>
            </div>
        </li>`;
    _parent.innerHTML += htmlStr;
}

function addEventToListItems() {
    var listItems = document.querySelectorAll('.list-item');
    listItems.forEach(function(listItem) {
        listItem.addEventListener('click', function(event) {
            var textContent = this.querySelector('a').innerText;
                copyToClipboard(textContent);
        });
    });
}

function copyToClipboard(text) {
    navigator.clipboard.writeText(text)
        .then(function() {
            // Do something... possible?
        })
        .catch(function(error) {
            console.error('Error copying text', error);
        });
}
