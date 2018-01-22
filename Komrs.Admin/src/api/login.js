const loginUrl = process.env.LOGIN_API;


const timeout = (ms) => {
    return new Promise(resolve => setTimeout(resolve, ms));
}

const base = (path) => {
    return loginUrl + path;
}

export const authenticate = async (email, password) => {

    try {
        let res  = await fetch(base('/user/login'), {
            method: "POST",
            body: JSON.stringify({ email, password }),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        }).then(res => res.json())

        return {
            ok: true, access_token : res.accessToken
        }

    } catch(e) {
        console.log("error", e);
        return { ok : false, message: "user not found" }
    }

}