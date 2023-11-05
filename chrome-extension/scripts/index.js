
async function main(){
    var _parent = document.getElementById('parent');

    await loadItemsAsync();

    async function loadItemsAsync(){
        var response = await fetch('https://localhost:7056/api/QuickPaste/items?n=5')
        var jsonResult = await response.json();

        var items = Array.from(jsonResult);

        for (let index = 0; index < items.length; index++) {
            await createChildAsync(`${items[index].text}`);
        }
        await addEventToListItemsAsync();
    }
    
    async function createChildAsync(str) {
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
    
    async function addEventToListItemsAsync() {
        var listItems = document.querySelectorAll('.list-item');
        listItems.forEach(function(listItem) {
            listItem.addEventListener('click', async function(event) {
                var textContent = this.querySelector('a').innerText;
                await copyToClipboardAsync(textContent);
            });
        });
    }
    
    async function copyToClipboardAsync(text) {
        navigator.clipboard.writeText(text)
            .then(function() {
                // Do something... possible?
            })
            .catch(function(error) {
                console.error('Error copying text', error);
            });
    }
    
}

main();