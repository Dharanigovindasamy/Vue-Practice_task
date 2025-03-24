import { BASE_URL } from "@/store/util";
import axios from "axios";

const getTaskUrl = axios.create({
    baseURL: BASE_URL
});

export const getTask = async () => {
    try {
        const response = await getTaskUrl.get("/tasks");
        return response.data;
    } catch (error) {
        console.error("Error in getTask", error);
    }
} 
