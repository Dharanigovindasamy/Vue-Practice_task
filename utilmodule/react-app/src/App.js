// src/App.js
import React from 'react';
import { greet } from 'dharani-util';

function App() {
  return (
    <div>
      <h1>{greet('Dharani')}</h1>
      {/* <p>3 + 4 = {add(3, 4)}</p> */}
    </div>
  );
}

export default App;
