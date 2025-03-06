import { Component, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgProgressbar } from 'ngx-progressbar';
import { NgProgressRouter } from 'ngx-progressbar/router';
import { HeaderComponent } from "../../components/shared/header/header.component";
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { SidebarComponent } from '../../components/shared/sidebar/sidebar.component';

@Component({
  selector: 'app-admin-layout',
  imports: [
    RouterModule,
    NgProgressbar,
    NgProgressRouter,
    HeaderComponent,
    MatSidenavModule,
    SidebarComponent
],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.scss'
})
export class AdminLayoutComponent {
  @ViewChild('sidenav') sidenav!: MatSidenav;
  isSidenavOpen = true;

  onSidenavClosed() {
    this.isSidenavOpen = false;
  }

  toggleSidenav() {
    this.sidenav.toggle();
    this.isSidenavOpen = !this.isSidenavOpen;
  }
}
