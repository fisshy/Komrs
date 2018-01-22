const storage = {
    setItem(key, value) {
        localStorage.setItem(key, JSON.stringify(value))
    },
    getItem(key) {
        return JSON.parse(localStorage.getItem(key))
    }
}

export default storage