import {expect, test} from "@playwright/test";


const myInlineData = [
    {bookTitle: "a", author: "sdlfjdfs", publisher: "asdsad", coverImgUrl: "lksjdlkfjdsf", testNumber: 1},
    {bookTitle: "asdsadsada", author: "", publisher: "asdsad", coverImgUrl: "lksjdlkfjdsf", testNumber: 2}
]

for (const book of myInlineData) {
    test('client side validation should make button disabled, test number: '+book.testNumber, async ({page}) => {
        await page.goto('/');
        await page.getByTestId('createBook').click();
        await page.getByTestId('titleInput')
            .locator('label')
            .locator('input')
            .fill(book.bookTitle);
        await page.getByTestId('authorInput')
            .locator('label')
            .locator('input')
            .fill(book.author)
        const button = await page.getByTestId('submit');
        await expect(button).toHaveAttribute('disabled', '');
    })
}





const myInlineDataSuccess = [
    {bookTitle: "aaksljdlasjk"+Math.random()*21389, author: "sdlfjdfs", publisher: "publisher_1", coverImgUrl: "https://picsum.photos/"+Math.floor(Math.random()*42)+1, testNumber: 1},
    {bookTitle: "asdsadsada"+Math.random()*1323921387137, author: "asdsadasd", publisher: "publisher_2", coverImgUrl: "https://picsum.photos/"+Math.floor(Math.random()*42)+1, testNumber: 2}
]

for (const book of myInlineDataSuccess) {
    test('successfully should be able to create a book, test number: '+book.testNumber, async ({page}) => {
        await page.goto('/');
        await page.getByTestId('createBook').click();
        await page.getByTestId('titleInput')
            .locator('label')
            .locator('input')
            .fill(book.bookTitle);
        await page.getByTestId('authorInput')
            .locator('label')
            .locator('input')
            .fill(book.author)
        await page.getByTestId('publisherInput')
            .locator('label')
            .locator('input')
            .fill(book.publisher)
        await page.getByTestId('coverImgUrlInput')
            .locator('label')
            .locator('input')
            .fill(book.coverImgUrl)
        const promise = page.waitForResponse('http://localhost:5000/api/books');
        await page.getByTestId('submit').click();
        const resolved = await promise;
        await expect(resolved.status()).toEqual(201);
        const bookCard = await page.getByTestId('card_'+ book.bookTitle);
        await expect(bookCard).toBeVisible();
    })
}

