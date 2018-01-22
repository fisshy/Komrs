import http from '../helpers/fetch';
const productUrl = process.env.PRODUCTS_API;


const base = (path) => {
    return productUrl + path;
}

export const listProductsApi = async () => {

    try {

        let products  = await http(base('/products'), {
            method: "GET"
        }).then(res => res.json())

        console.log("products", products);

        return products;

    } catch(e) {
        console.log("error", e);
        return { message: "failed to list products" }
    }

}