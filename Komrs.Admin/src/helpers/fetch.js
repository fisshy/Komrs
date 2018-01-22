const http =  async (url, options) => {

    let access_token = localStorage.getItem('access_token');

    if(!options.headers) {
        options.headers = new Headers({
            "Authorization" : `Bearer ${access_token}`
        })
    } else {
        options.headers.append("Authorization", `Bearer ${access_token}`);
    }


    return fetch(url, options);
}

export default http;