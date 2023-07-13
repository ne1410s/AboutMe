import { Component } from '@angular/core';

import { ProjectMetadataService } from 'src/app/features/projects/services/project-metadata.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
})
export class MenuComponent {

  projects$ = this.projectService.getProjects();

  constructor(private projectService: ProjectMetadataService) {}
}
