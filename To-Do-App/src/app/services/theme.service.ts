import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private readonly THEME_KEY = 'theme-preference';

  constructor() {
    this.loadTheme();
  }

  toggleTheme() {
    const currentTheme = document.documentElement.getAttribute('data-bs-theme');
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
    document.documentElement.setAttribute('data-bs-theme', newTheme);
    document.documentElement.classList.toggle('theme-dark', newTheme === 'dark');
    localStorage.setItem(this.THEME_KEY, newTheme); // Guardar en localStorage
    
      
  }

  loadTheme() {
    const savedTheme = localStorage.getItem(this.THEME_KEY) || 'light';
    document.documentElement.setAttribute('data-bs-theme', savedTheme);
    document.documentElement.classList.toggle('theme-dark', savedTheme === 'dark');
  }
}
