const express = require("express");
const cors = require("cors");

const app = express();
app.use(cors()); 

const users = [
    { id: 1, name: "Dharani", email: "dharani@gmail.com" },
    { id: 2, name: "Divya", email: "divya@gmail.com" },
    { id: 3, name: "Dharshini", email: "dharshini@gmail.com" },
];

// API endpoint to get users
app.get("/api/users", (req, res) => {
    res.json(users);
});

// Start the server
const PORT = 5000;
app.listen(PORT, () => {
    console.log(`Server running on http://localhost:${PORT}`);
});
