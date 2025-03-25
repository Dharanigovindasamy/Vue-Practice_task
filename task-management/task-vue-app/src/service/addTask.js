import { BASE_URL } from "@/store/util";
import axios from "axios";

const addTaskUrl = axios.create({
    baseURL: BASE_URL
});

export const addTask = async (task) => {
    try {
        const response = await addTaskUrl.post("/AddTask", task, 
             { headers: {
            "Content-Type": "application/json" 
        }}
        );
        console.log("Task added successfully", response.data);
        return response.data;
    } catch (error) {
        console.error("Error in addTask", error);
    }
}
