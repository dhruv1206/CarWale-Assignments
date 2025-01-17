import { render, screen } from '@testing-library/react';
import App from './App';

test('renders home page', () => {
  const screen = render(<App />);
  const linkElement = screen.getByText(/Used Cars in India/i);
  expect(linkElement).toBeTruthy();
});
