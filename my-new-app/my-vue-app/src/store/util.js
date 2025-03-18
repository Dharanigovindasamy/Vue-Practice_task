export const LOCAL_API_URL = "http://localhost:5000";
export const mockAPI_URL = "https://jsonplaceholder.typicode.com";

export const highlightMatch = (searchQuery, text) => {
    if (searchQuery.value.length < 2) return text;
    const regex = new RegExp(`(${searchQuery.value})`, "highlight match");
    return text.replace(regex, `<mark>$1</mark>`);
};