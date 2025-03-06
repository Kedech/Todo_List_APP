import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ThemeService } from './services/theme.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, MatButtonModule, MatIconModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'To-Do-App';
  isDarkTheme = false;

  constructor(
    private themeService : ThemeService
  ) {
    this.themeService.loadTheme();
    //this.isDarkTheme = document.documentElement.getAttribute('data-bs-theme') === 'dark';
    this.isDarkTheme = document.documentElement.classList.contains('theme-dark');
  }
  
  toggleTheme() {
    this.themeService.toggleTheme();
    //this.isDarkTheme = document.documentElement.getAttribute('data-bs-theme') === 'dark';
    this.isDarkTheme = document.documentElement.classList.contains('theme-dark');
  }
}
