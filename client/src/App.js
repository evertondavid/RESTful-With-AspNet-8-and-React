import React from 'react';
import './global.css';
import Routes from './routes';
//JSX code: HTML-like syntax that is transformed to JavaScript XML
//JSX is not a necessity, but it is a popular choice
// Inject JavaScript code into JSX using curly braces {}
export default function App() {
  return (
    <div>
      <Routes />
    </div>
  );
}
